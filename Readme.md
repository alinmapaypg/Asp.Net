# Alinma Pay .NET Hosted PG Integration

This repository contains the **.NET Framework integration kit** for
**Alinma Pay Business Hosted Payment Gateway**.

It enables merchants to securely collect payments using:

-   Credit Cards\
-   Debit Cards\
-   Card Tokenization\
-   Apple Pay\
-   Other supported payment methods

The integration ensures a secure, seamless, and PCI-compliant payment
experience.

------------------------------------------------------------------------

## 🛠 Specifications

  Item               Details
  ------------------ ----------------
  Framework          .NET Framework
  Version            4.5
  Document Version   3.0.3
  Last Updated       Feb 10, 2026

------------------------------------------------------------------------

## 📋 Prerequisites

Before starting the integration, complete the following steps in the
**Alinma Pay Merchant Portal**:

1.  Create a Merchant Dashboard Account\
2.  Obtain API Credentials:
    -   Terminal ID\
    -   Terminal Password\
    -   Merchant Key\
3.  Configure Integration Settings:
    -   Update `Key.xml` with credentials, transaction URL, and currency
        settings

------------------------------------------------------------------------

## ⚙️ Configuration

You can configure the application using either:

-   `Key.xml`
-   `appconfig.json`

### Sample JSON Configuration

``` json
{
  "Hosted": "Merchant Terminal ID",
  "Password": "Merchant Password",
  "Secret": "Merchant Key",
  "Url": "Test or Production URL"
}
```

------------------------------------------------------------------------

## 💳 Supported Operations

### 1️⃣ Purchase & Refund

-   Standard payment collection\
-   Full or partial refunds

### 2️⃣ Authorization

-   Pre-Authorization\
-   Capture

### 3️⃣ Voiding

-   Void Purchase\
-   Void Refund\
-   Void Capture\
-   Void Authorization

### 4️⃣ Management

-   Transaction Inquiry\
-   Card Tokenization:
    -   Add Card\
    -   Update Card\
    -   Delete Card

### 5️⃣ Alternative Payments

-   Apple Pay

------------------------------------------------------------------------

## 📝 Integration Flow

### Step 1: Payment Initiation

When the customer clicks the **Payment** button, they are redirected to:

    Payment.aspx

### Step 2: Data Entry

The customer enters personal and order details.

### Step 3: Place Order

Clicking **Place Order** sends the transaction request to the Alinma Pay
Gateway.

------------------------------------------------------------------------

## 🔑 Key Request Parameters

  -----------------------------------------------------------------------
  Parameter                         Description
  --------------------------------- -------------------------------------
  Country                           Customer country (Mandatory)

  Email                             Customer email address (Mandatory)

  Currency                          ISO currency code (Mandatory)

  Amount                            Transaction amount (Mandatory)

  Action                            Transaction type (Purchase, Refund,
                                    Authorization, etc.) (Mandatory)
  -----------------------------------------------------------------------

------------------------------------------------------------------------

## 🔗 Documentation

For detailed API specifications, response codes, and integration guides:

Merchant Portal → Developer → API Keys → Developer Integration Guide

------------------------------------------------------------------------

## 🚀 Environment URLs

Use the appropriate gateway URL:

-   Test Environment -- For development and testing\
-   Production Environment -- For live transactions

Configure inside `Key.xml` or `appconfig.json`.

------------------------------------------------------------------------

## 📞 Support

For technical assistance, contact Alinma Pay support through the
Merchant Portal.
