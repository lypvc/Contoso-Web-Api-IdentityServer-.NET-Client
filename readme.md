## Contoso University Web Api Protected by IdentityServer4 - ##
## Consumed by .NET Client ##

**Level:** Beginner

**Intention:** Basically show how to consume a protected API from a .NET Client (learning stuff)

I am big fan of **IdentityServer**, and this repo is built on top of/using 
one of their samples, coming from [here](https://github.com/IdentityServer/IdentityServer4.Samples).

The classes used come from **Microsofts** excellent EF Guide, can be found at this [spot](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro).

The code concerning the Contoso University API originates from this repo, by [kkagill]( https://github.com/kkagill/ContosoUniversity-Backend).  **This repo has some very nice examples on how to...**

1. make calls into the db
2. use the Generic Repository Pattern 
3. use Interface implementation - **take a close look, this set up gives a lot of flexibility**
4. add pagination on the API, through the usage of headers in this case

**Validation:** we can take whatever approach we like, but **Fluent Validation** used in the original 
above, will serve the purpose. 

**Finally what I wanted to do:** I wanted to consume data from an Web Api(not just inspect the tokens), protected by IdentityServer, using an MVC application, adding simple authorization into the mix.

**MvcClientB**  is the .NET client consuming the ContosoUniversity.API.  
My intention was to use it with some kind of linear, DRY and elegant builder pattern, like this one being used in [here]( https://github.com/TahirNaushad/Fiver.Api.HttpClient), but I could not found a way of how to extract the token and then use it in this fashion. 


**Note:** When using HttpClient, the recomendation from Microsoft is to use only one instance, hence the DI. 

**-- Steps to try this demo out --** 

1. Download, rebuild.
2. Be sure to set Start Up Project to "current selection" from the properties
    of the Solution.
3. Create a local db for the Contoso Context by marking ContosoUniversity API as the
    Start Up, and then from Package Manager Console, make sure ContosoUniversity.Data - where
    I store these migrations - are selected. Run: update-database -context ContosoContext
   
4. Seed this Db by F5(running) ContosoUniversity API.

5. Next, repeat the process of updating another Db for the users of the IdentityServer, by marking
   this project as Start Up, and from Package Manager Console, update the 3 different contexts,
   respectively:  ApplicationDbContext, PersistedGrantDbContext and ConfigurationDbContext.

6. Seed. From the rootfolder of the IdentityServer project, open command prompt and run: 
   dotnet run /seed. 
   Then run the project via F5 once to populate with 2 users. 

7. To test the flow, restart the IdentityServer, then ContosoUniversity API and finally 
    the MvcClientB client. 
   
   Log in on MvcClientb by going to Secure tab, provide: **"user2@gmail" and "PassW0rd!".** 
    
   The original consent screen provided by **IdentityServer** is still set to true (is showing), 
   but accept/move on and inspect the information coming from the tokens.
   **There should be a claim saying user2 is an administrator.**

   **Try it out:** 	
 
   a) from the MvcClientB, go to students tab, currently changeing the initial pagination size. This tab      
      loads the data if everything works (could not get Course Title to populate though, but in Postman
      it does..)

     **Note:** Only user2 is an administrator at this point. Authorization is set on the receiving students
     controller on the ContosoUniversity.API.

  b) copy the token and use Postman to try it out. Don't forget to set the headers!
      - or if you like, test by implementing **Swagger.** Another good tool is found at **jwt.io**, used for
     inspection of the token.
   
  c) lab around, improve (lots of rooooom)


**Note:** only the students controller are ready in the MvcClientB

**Next:** JavaScript client to consume, just for comparison which is the better approach - but my hunch is the JavaScript client, Angular-built if time.

