# 🚗 Parking Management Mini-System (Web API + Angular)

This project is a Full-Stack solution developed to manage vehicle entry and exit, supporting pricing requirements for operations in Brazil and Argentina.

## 🛠️ Technologies Used

* **Back-End:** ASP.NET Core Web API (.NET 10.0) with C#
* **Front-End:** Angular 19+
* **Database:** SQLite with Entity Framework Core
* **Architecture:** Dependency Injection for tax and fee calculation (Strategy Pattern)

## 🧠 System Logic

The application was designed with a focus on separation of responsibilities:

* **Vehicle:** Stores unique vehicle registration information
* **Parking Session:** Manages vehicle entry and exit status
* **Invoice:** Generated when the vehicle exits, calculating the total amount based on parking duration and pricing rules

## 📌 Pricing Rule (Rounding Logic)

For this challenge, the following rounding logic was implemented:

**Full Hour Charging:** After the first 60 minutes, any fraction of an additional hour (e.g., 1 hour and 10 minutes) is rounded up and charged as a full extra hour. This approach helps maintain operational sustainability and simplifies pricing for end users.

## 🚀 How to Run the Project

### Back-End

Navigate to:

```text
BackEnd/MiniSistemaEstacionamentoAPI
```

Run:

```bash
dotnet run
```

The API will be available at:

```text
http://localhost:5000
```

(or according to the `launchSettings.json` configuration)

### Front-End

Navigate to:

```text
FrontEnd/projectangular
```

Run:

```bash
npm install
npm start
```

Access the application at:

```text
http://localhost:4200
```
