<p align="center">
  <img src="BankWebApp/wwwroot/images/logo/DozenDozen-logo-alternate.svg" alt="Dozen & Dozen Bank Logo">
</p>

# Dozen & Dozen Bank
  a web app project by William Gross

## Table of Contents
1. [Introduction](#introduction)
   - [Azure](#azure)
   - [User Logins](#user-logins)
2. [Backend](#backend)
   - [Database](#database)
   - [ViewModels & Services](#viewmodels--services)
   - [Libraries](#libraries)
3. [Frontend](#frontend)
   - [Design](#design)
   - [Pages](#pages)
     - [Landing Page](#landing-page)
     - [Users](#users)
     - [Customers](#customers)
       - [All Customers Page](#all-customers-page)
       - [Customer Profile](#customer-profile)
       - [Account Page](#account-page)
       - [Transactions](#transactions)
4. [Input Validation](#input-validation)

## Introduction
### Azure
The link to the app, hosted on Azure: https://dozendozenbank.azurewebsites.net/

### User Logins
- Admin: richard.chalk@systementor.se | Password: Hejsan123#
- Cashier: richard.erdos.chalk@gmail.se | Password: Hejsan123#
- Super user: william@gross.nu | Password: Hejsan123#

 ## BACKEND

### DATABASE
The web application was built database first, making use of the database provided by the course.

Do note, that I made adjustments to the database to allow activation/deactivation of users, so please make sure to reset the database completely before and after grading my project, so my project doesn't interfere with other student's projects and vice verse.

### VIEWMODELS & SERVICES
In order to not expose the database to the website, I've utilised viewmodels, and abstracted as much logic away from the razor page cshtml to services using interfaces.

### LIBRARIES
I ran into some problems with circular references quite early when I started to use libraries. Luckily, I could solve it without having to start the project over from scratch.

The solution I came up with was to point a reference from the application to the DataAccessLayer, and one to my Services library, as well as another set of references from my Services to the DataAccessLayer and my ViewModels.

## FRONTEND

### DESIGN
Not wanting my app to feel too "bootstrappy" I opted early on to design the website myself. At first I used an old project as a starting point, but quickly grew tired of it and changed more or less everything to my own design.

I went for a simplistic dark mode design, not wanting to make it tacky. I guess it didn't turn out very "banky" in the end, but hey, less is more.

### PAGES

#### LANDING PAGE
The landing page consists of a simple hero and a statistics section. The stats by country cards were part of VG criteria, but the statistics section felt too empty without it, so I added a simplified version of it without the required VG logic.

#### USERS
To access this page, you must be logged in as an admin.

The users CRUD, being VG criteria too, I didn't fully implement the funtionality to create, view and edit users. Not wanting to just link to an empty page, I just added a simple table which displays all users on the system, no further functionality.

#### CUSTOMERS
The meat and bones for the project. In order to access this page you must be logged in as a cashier.

##### ALL CUSTOMERS PAGE

- From here the user can find a paginated table of all the bank's customers. The list of customers can be filtered by country, it can also be sorted by name, country, city and the customer's active status (deleted or not).

- One can also search for customers by ID, name, city, country as well as national ID.

- Above the customer table is a button to create new customers.

- On the right side of the page there is a list of the bank's top 5 customers, as the bank likes to keep tabs on their largest cash cows.

- The user can press the chevron to the right of each customer to go to their profile.

##### CUSTOMER PROFILE
- The customer profile view consists of a summary, an overview of all the user's accounts and their latest transactions, a card with the customer's information, a button to activate/deactivate the user as well as buttons to add and remove accounts.
- An account can only be deleted once it's been emptied of it's funds.
- There are quick link buttons to the different types of transactions above each account summary. These same buttons appear in the detailed account page, as well as on the transaction pages.

##### ACCOUNT PAGE
- The account page consists of a simple table displaying the accounts transactions, sorted chronologically with the latest transactions appearing first.
- The "Load more" button utilizes JS/AJAX.

##### TRANSACTIONS
- The available transaction types are deposit, withdrawal and transfer.
- The different transaction pages all work the same, only differing in their appropriate input validation.

### INPUT VALIDATION
All input forms utilize both client-side and server-side validation where applicable.

For some reason, the client-side is a little wonky. Sometimes it works, sometimes it doesn't. Though, I've been made aware that most likely is not because of my app, and more a strange issue with JS/Ajax doing it's own thing.
