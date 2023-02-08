# OnlineAuction-GW

API to work on Auction

It has:
 - Background task (Hangfire) that is going to National Belarus Bank API to get the latest currency pair rates;
 - In-memory cache for static db-objects that represented as static classes;
 - Work with db is based on EntityFrameworkCore, but with DB-first approach;
 - Cloudinary service usage for storing user's profile image;
 - Selfmade auth service, based on JWT tokens;
 - Custom exception handling.
