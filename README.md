# PizzaMaker
Site for portfolio, sale pizza, Ninja Turtles will be satisfied

## Requirements

### Application requirements

- The menu will be visible to the customer with the pizzas. All the ingredients will be shown with their prices. 
- After selecting a desired pizza, customers can view the details of pizza such as price, category and toppings. 
- Customer can specify the extra toppings (if required) and enter the quantity required and add a pizza to cart. 
- Customer can directly click on buy now to place an order. 
- Now payment option is shown to the customer. He has to choose from the various online payment methods or cash on delivery option. 
- Once the order is placed and order id will be provided to the customer using which he/she can track the ordered pizza. 
- Customer can notify the admin about the system by writing a feedback message. 
- Customers can change their old password with new one whenever required. 
- Admin can add various pizzas into the system, also can view/edit/delete the added pizza. \
- All the orders will be displayed to the admin where admin can update the order details. 
- Admin can view the pizza sales details based on any two selected dates. 
- Also can view feedback messages received from the registered customers.

### Roles requirements
1. **Admin**:
   1. Add pizza: add different types of pizza’s in veg and non-veg category. 
   2. View/Edit/Delete: Can view/update/delete the added pizza from the database. 
   3. View/Update Order: Can view all the orders received from the customer and change the order details accordingly. 
   4. View Sales: Can view sales details based on any two between dates. 
   5. View Feedback: Admin can see user’s feedback.
2. **User**:
   1. Registration: User can register his detail. 
   2. Login: User Login his account. 
   3. Home page: User can visit his home page. 
   4. Pizza Detail: Can view pizza details by selecting a pizza and view its details such as price, toppings, etc… 
   5. Add to Cart: Customer can add the selected pizza into cart and can check out further. 
   6. Buy Now: Can buy a selected pizza and can also enter required toppings (If needed) and specify the quantity. 
   7. Track Order: Can track the ordered pizza by order id to know the order status. 
   8. Write Feedback: User can write his review as feedback. 
   9. Change Password: User can change his current password and make new password.

## Technology stack

- **Blazor**: it was selected due to good balance between SEO optimization and easiness interactivity support.
- **MS SQL**: as we use Microsoft technology, I decided to stay in Microsoft development ecosystem.
- **Redis**: for storing user state.
- **Docker**: for easy development deployment.