# 🔧 FixHub - DIY Repair & Technician Service Platform

FixHub is a multi-functional web-based platform designed to support users in repairing electronic devices, sharing repair knowledge, and connecting with professional technicians. It brings together a DIY community with service features to meet real-life repair needs.

## 📌 Features

### 👤 For Users (Customers)
- ✅ Register/Login and manage profile
- 🔍 Browse and read detailed step-by-step repair guides (with images/videos)
- 🛠 Create service requests for technical support
- 💬 Post and comment on the community forum
- 💳 Redeem points to access premium content or pay for services
- 🕒 View repair history and track progress

### 🧰 For Technicians
- 📬 Receive and manage assigned repair requests
- ✍️ Write and edit repair guides
- 📈 Earn reputation points based on completed services
- 💬 Respond to user feedback and questions

### 🛠 For Administrators
- 👥 Manage users and technician accounts
- 📂 Moderate forum posts and repair content
- 📊 View system statistics and activity logs
- 📑 Categorize repair guides by difficulty and device type

## 🏗️ Technology Stack

- **Frontend:** HTML/CSS, Bootstrap, Razor Pages
- **Backend:** ASP.NET Core MVC
- **Database:** SQL Server
- **Authentication:** Identity Framework
- **Architecture:** MVC (Model-View-Controller)

## 🗂️ Database Schema Highlights

- `Users` – Stores user and technician profiles  
- `RepairGuides` – Repair tutorials with media content  
- `ServiceRequests` – Records service orders and their status  
- `ForumPosts` / `ForumComments` – Forum discussions and replies  
- `Technicians` – Technician accounts and skill ratings  
- `Products` – Repair tools and parts available for purchase

## 📊 Use Case Overview

FixHub supports 3 main user roles:

- **Customer:** Browses guides, requests services, interacts in forum  
- **Technician:** Handles repair requests, writes guides  
- **Admin:** Manages content, users, and oversees system operation

(See `docs/UseCaseDiagram.png` for full diagram)

## 🚀 Getting Started

### Requirements
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server 2019+
- Visual Studio 2022 or newer

### Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/fixhub.git
