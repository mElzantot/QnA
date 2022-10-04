# Welcome To QnA !

## Main Features

- App will support many users who can **add question and answer**on others question
- users also can **upvote and downvote** others answers

## QnA Structure

- DBModels dll
     > contains app database entites
- DAL dll
    > connect between app and or DB using Ef core ORM
- BAL dll
    > encapsulate app business logic
    
- QnA Web APi project
    > contains app Apis that willbe used by client

- QnA Testing project
   > app unit testing methods
   > 
## Used Tools :

1. **JWT Token**  -> Auth
2. **Xunit**  -> unit testing
3. **EF Core** -> ORM

- 
## Postman collection: 
To download postman collection  [click here](http://www.getpostman.com/collections/02b56824487ad13f98a0)


## Docker

Docker file is exist in QNA -Web APi- project root .
to run it first you need to run CMD in solution folder then use this command to build 
 > docker build -f ./QnA/Dockerfile -t qna .


## Testing users
 --> Users ["Ahmed" , "Islam" , "Mustafa" , "Maha" , "Aya"]
 --> Passwoed : "SaaS1"
  > also there is a register end point you can use it to add new users 

