# Dozen & Dozen Bank
  a web app project by William Gross

## First things first
 ### Azure
The link to the app, hosted on Azure: https://dozendozenbank.azurewebsites.net/

 ### User Logins
- Admin: richard.chalk@systementor.se | Password: Hejsan123#
- Cashier: richard.erdos.chalk@gmail.se | Password: Hejsan123#
- Super user: william@gross.nu | Password: Hejsan123#

 ### Developer's commentary
 Shortly after this course began, my friend told me he had just bought tickets to go to Japan, and proposed I come with him. Having always wanted to go there for my photography, I couldn't pass up on this opportunity.
 To be able to go on the trip, without interrupting my education, I spent a little more than a week working very hard on the project, from early morning to late at night, only leaving my appartment once to buy groceries.
 It was exhausting, to say the least, but when is a good crunch not exhausting?

 I kept studying and working on the project while away, but there were too many things to see, too many things to do and too many beers to drink. The stress of managing studying with pleasure grew too large, and the project suffered because of it
 Somewhere between Osaka and Tokyo I decided to lower the goals I had set for myself, and only aim for a G in this assignment.
 
 Though I am a bit disapointed in myself, I made sure it would be the best damn G you'll grade in this course. That's why you'll se a bunch of things that were only required for VG, as well as some extra features for flair, like the filtering function on the customers page or the (non functional) contact page. Hope you're not too disappointed!

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
