# Pagos Móviles Project Overview

**Pagos Móviles** is a digital payment system designed to facilitate seamless financial transactions between users of the same or different banks. Developed using a distributed architecture with multiple components and technologies, the system supports efficient payment processing and user account management.

---

## Core Components

### External Receiver
- **Technology Stack:** C#, .NET 8.0, MySQL  
- **Role:** Handles incoming payment requests and communicates with the orchestrator to process transactions.

### Orchestrator
- **Technology Stack:** Python 3.12, MongoDB  
- **Role:** Acts as the central coordinator, managing communication between the external receiver, banking services, and additional web services.

### Banking Service
- **Technology Stack:** Java 21, SQL Server  
- **Role:** Processes the actual banking transactions and interacts with the database for account verification and updates.

---

## Web Services

### Administration Web Service
- **Technology Stack:** C#, .NET Framework 4.8.1, MongoDB  
- **Role:** Provides administrative functions, such as user and account management.

### User Web Service
- **Technology Stack:** Java 17, SQL Server  
- **Role:** Offers user-focused functionalities, such as balance inquiries and transaction history.

---

## Web Interfaces

### Administrator Web Application
- **Technology Stack:** .NET, C#, Web Forms  
- **Features:**
  - Manage users  
  - Manage accounts  

### User Web Application
- **Technology Stack:** .NET, C#, Web Forms  
- **Features:**
  - Check account balances  
  - Make payments to users within the same bank or other banks   
