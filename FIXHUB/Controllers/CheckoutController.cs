using FIXHUB.Models;
using FIXHUB.Services;
using FIXHUB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FIXHUB.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly FixHubDbContext _context;
        private readonly IVnpayServices _vnpayServices;

        public CheckoutController(FixHubDbContext context, IVnpayServices vnpayServices)
        {
            _vnpayServices = vnpayServices;
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            int userID = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            var user = _context.UserPoints.FirstOrDefault(u => u.UserId == userID);
            var usertransactions = _context.TopUpTransactions
                .Where(t => t.UserId == userID)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
            ViewBag.PointBalance = user?.PointBalance ?? 0;
            ViewBag.UserTransactions = usertransactions;
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Checkout(CheckoutVM model)
        {
            int userID = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            var user = _context.Users.FirstOrDefault(u => u.UserId == userID);

            if (model.Amount <= 0)
                return View("Error", "Số tiền không hợp lệ.");

            if (model.PaymentMethod == "Thanh toán VNPay")
            {
                var vnPayModel = new VnPaymentRequestModel
                {
                    Amount = (double)model.Amount,
                    CreatedDate = DateTime.Now,
                    Description = $"Nạp tiền: {user.UserName} {user.Email}",
                    FullName = user.UserName,
                    OrderId = new Random().Next(1000, 100000)
                };

                var paymentUrl = _vnpayServices.CreatePaymentUrl(HttpContext, vnPayModel);

                // Trả về redirect thay vì Json
                return Redirect(paymentUrl);
            }

            return View("Error", "Chưa hỗ trợ phương thức thanh toán này.");
        }
        [Authorize]
        public IActionResult PaymentCallback()
        {
            // Lấy phản hồi từ VNPay (tuỳ thuộc _vnpayServices.ProcessResponse trả về gì)
            VnPaymentResponseModel response = _vnpayServices.PaymentExecute(Request.Query);

            if (!response.Success || response.VnPayResponseCode != "00")
            {
                return RedirectToAction("PaymentFail", "Checkout");
            }

            int userID = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            
            decimal amount = response.Amount;
            //if (!decimal.TryParse(response.OrderDescription.Split(" ").FirstOrDefault(), out amount))
            //{
            //    return RedirectToAction("PaymentFail", "Checkout");

            //}

            // Tính số điểm (1.000đ = 1 điểm)
            var points = (int)(amount / 1000);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // 1. Lưu giao dịch vào bảng TopUpTransactions
                    var topUp = new TopUpTransaction
                    {
                        UserId = userID,
                        Amount = amount,
                        Points = points,
                        PaymentMethod = "VNPay",
                        TransactionCode = response.TransactionId,
                        Status = "Success",
                        CreatedAt = DateTime.Now
                    };
                    _context.TopUpTransactions.Add(topUp);

                    // 2. Cộng điểm vào UserPoints
                    var userPoint = _context.UserPoints.FirstOrDefault(x => x.UserId == userID);
                    if (userPoint == null)
                    {
                        _context.UserPoints.Add(new UserPoint
                        {
                            UserId = userID,
                            PointBalance = points
                        });
                    }
                    else
                    {
                        userPoint.PointBalance += points;
                        _context.UserPoints.Update(userPoint);
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    return RedirectToAction("Success", "Checkout");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return RedirectToAction("PaymentFail", "Checkout");
                }
            }
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult PaymentFail()
        {
            return View();
        }


    }
}
