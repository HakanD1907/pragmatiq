using Microsoft.EntityFrameworkCore;

class Program {
    public static void Main()
    {
        //ToDo 0: BulkDataSeeding must execute succssfully at least once
        //BulkDataSeeding();
        // Solve each exercise individually to avoid making Main jumbled up.
        // call methods to solve each exercise here
        using (var context = new HotelContext())
        {
            //Call method corrospoindg to each exercise.
            q1(context);
            // .... 
            q8b(context);
            
            
            ////Solution will not be available during actual exam.
            //Solution.q1Solution(context);
            //Solution.q2Solution(context);
            //Solution.q3Solution(context);
            //Solution.q4Solution(context);
            //Solution.q5Solution(context);
            //Solution.q6Solution(context);
            // //Exercise 7 in Model
            //Solution.q8aSolution(context);
            //Solution.q8bSolution(context);
        }
    }
        
    static void BulkDataSeeding(){
        using (var dbContext = new HotelContext()) {
            if (dbContext == null)
            {
                Console.WriteLine("No DbContext avaialable");
                return;
            }
            if (dbContext.bookings.Count() > 0)
            {
                Console.WriteLine("Database already contains data empty database if seeding is required");
                return;
            }
            
            dbContext.roomType.AddRange(SeedData.SeedRoomType());
            dbContext.rooms.AddRange(SeedData.SeedRoom());
            dbContext.guests.AddRange(SeedData.SeedGuest());
            dbContext.bookings.AddRange(SeedData.SeedBookings());
            Console.WriteLine("{0} Records Created.", dbContext.SaveChanges());
        };

    }
    
    static void q1(HotelContext db) {
        //ToDo 1: solve exercise 1 here
        // Exercise 1: Give the booking detail of given guest booking details (for GuestID 10).  The result should include booking date, room number, and number of nights. 	
        var opdr1 = from booking in db.bookings
                        where booking.GuestID == 10
                        select booking;

        foreach (var row in opdr1)
            Console.WriteLine($"{row.BookingDate}, {row.RoomNumber}, {row.Nights}");
        
    }
    
    static void q2(HotelContext db)
    {
        //ToDo 2: solve exercise 2 here
        //Exercise: 2:  List down all the guest names, and room number, having booking on specific date (2022-01-31) 
    }
  
    static void q3(HotelContext db)
    {
        //ToDo 3: solve exercise 3 here
        //Exercise 3: List down number of bookings per day where there are more than 1 bookings
    
    }
    static void q4(HotelContext db)
    {
        //ToDo 4: solve exercise 4 here
        //Exercise 4: List the rooms that are free (not booked) on '2022-01-13'

    }


    static void q5(HotelContext db)
    {
        //ToDo 5: solve exercise 5 here
        //Exercise 5: 
    }

    static void q6(HotelContext db)
    {
        //ToDo 6: solve exercise 6 here
        //Exercise 6: 
        

    }


    //Ex7 in Model.cs

    static void q8a(HotelContext db)
    {
        //ToDo 8a: solve exercise 8a here
        //Exercise 8a: 

    }
    static void q8b(HotelContext db)
    {
        //ToDo 8b: solve exercise 8b here
        //Exercise 8b: 

    }
    

}