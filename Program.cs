using Microsoft.EntityFrameworkCore;

class Solution {
    
    public static void q1Solution(HotelContext db)
    {

        //Exercise 1: Give the booking detail of given guest booking details (for GuestID 10).  The result should include booking date, room number, and number of nights. 	
        /*
         SQL:
            SELECT BookingDate, RoomNumber, Nights 
            FROM Bookings
            WHERE GuestID= 10;
         */
        //Raw SQL 1
        //var guestBookings = db.bookings.FromSqlRaw("SELECT * FROM Bookings WHERE \"GuestID\" = 10; ").ToList();

        //LINQ Method
        var guestBookings = db.bookings.Where(x => x.GuestID == 10);

        //LINQ expression
        //var guestBookings = from booking in db.bookings
        //                    where booking.GuestID == 10
        //                    select booking;

        //Output
        //guestBookings.ForEach(row => Console.WriteLine($"{row.BookingDate}, {row.RoomNumber}, {row.Nights}"));
        //OR
        foreach (var row in guestBookings)
            Console.WriteLine($"{row.BookingDate}, {row.RoomNumber}, {row.Nights}");

    }

    public static void q2Solution(HotelContext db)
    {
        //Exercise: 2:  List down all the guest names, and room number, having booking on specific date(2022 - 01 - 31) 	
        /* SQL
         * 
         SELECT g.Name, r.Number
         FROM Bookings b JOIN Guests g on b.GuestID=g.ID
         WHERE b.BookingDate = '2022-01-31'
         */
        
        /*var guestRoom = db.bookings.Where(b=>b.BookingDate== new DateOnly(2022,01,31))
                        .Include(b=>b.guest)
                        .ToList();
        foreach (var g in guestRoom) {
            Console.WriteLine($"{g.guest.Name}, {g.RoomNumber}");
        }
        */
        var guestRoom = from booking in db.bookings
                        join guest in db.guests
                        on booking.GuestID equals guest.Id
                        where booking.BookingDate == new DateOnly(2022, 01, 31)
                        select new { booking, guest };
        foreach (var g in guestRoom)
        {
            Console.WriteLine($"{g.guest.Name}, {g.booking.RoomNumber}");
        }

    }

    public static void q3Solution(HotelContext db)
    {
        //Exercise 3: List down number of bookings per day where there are more than 1 bookings
        /*
         Select BookingDate, count(*), array_agg(roomnumber)
         from bookings
         group by 1
         having count(*)>1
         */
        var query3 = from b in db.bookings
                     group b by b.BookingDate into bdg
                     where bdg.Count() > 1
                     select new { K = bdg.Key, V = bdg.Count() };
        //output
        foreach (var x in query3)
            Console.WriteLine($"{x.K}, {x.V}");

        var q3Complex = from b in db.bookings
                        group b by b.BookingDate into BookingGroup
                        where BookingGroup.Count() > 1
                        select new
                        {
                            K = BookingGroup.Key,
                            c = BookingGroup.Count(),
                            agg = string.Join(',', (from r in BookingGroup select r.RoomNumber).ToList())
                        };
        //output
        foreach (var x in q3Complex)
            Console.WriteLine($"{x.K}, {x.c}, {x.agg}");
    }

    public static void q4Solution(HotelContext db)
    {
        //Exercise 4. List the rooms that are free on '2022-01-13'.
        /*
         SELECT Number FROM rooms  WHERE Number not in
         	(SELECT roomnumber FROM bookings WHERE bookingdate='2022-01-13')
         */
        Console.WriteLine("Using subquery");
        var subq4 = from room in db.rooms
                     where (from b in db.bookings
                            where b.BookingDate == new DateOnly(2022, 1, 13)
                            select b.RoomNumber).Contains(room.Number) == false
                     select room.Number;
        //output
        foreach (var x in subq4)
            Console.WriteLine(x);

        Console.WriteLine("Set Operations using Except");
        var Setq4 = db.rooms.Select(r => r.Number).ToList()
            .Except(db.bookings.Where(b => b.BookingDate == new DateOnly(2022, 1, 13)).Select(_ => _.RoomNumber).ToList());
        //output
        foreach (var y in Setq4)
            Console.WriteLine(y);

    }

    public static void q5Solution(HotelContext db) {
        //Exercise: 5:  List down top 5 valued customers, with their id and spending 
        //HINT: a valued customer is the on with max amount spent, amount = Nights * Price for each booking of a customer

        /*
         Select GuestID,  sum(nights*price), array_agg(nights||'*'||Price)
         FROM Bookings b JOIN Rooms r on b.roomnumber=r.Number
		 JOIN RoomType rt on r.RoomTypeID = rt.ID
         group by 1
         order by 2 desc
         limit 5
         */

        var q5Sub = from b in db.bookings
                    join r in db.rooms on b.RoomNumber equals r.Number
                    join rt in db.roomType on r.RoomTypeId equals rt.Id
                    group b by b.GuestID into bookingroomgroup
                    select new
                    {
                        GID = bookingroomgroup.Key,
                        Spending = bookingroomgroup.Sum(_ => _.Nights * _.room.roomType.Price)
                                    //(from bk in bookingroomgroup
                                    //select bk.Nights * bk.room.roomType.Price)
                    };
        
        var q5 = q5Sub.OrderByDescending(_ => _.Spending).Take(5).ToList(); //Query syntax does not support take
        q5.ForEach(_=>Console.WriteLine($"{_.GID}, {_.Spending}"));
        
    }

    public static void q6Solution(HotelContext db)
    {
        //Exercise 6: 
        var All = db.roomType.Select(rt=>new { RoomType = rt.Type,RoomNumbers = rt.Rooms.Select(_=>_.Number) });
        var booked = db.bookings.Where(_ => _.BookingDate == new DateOnly(2022, 1, 13)).Select(_=>_.RoomNumber).ToList();

        Console.WriteLine("Booking Status");
        foreach (var x in All)
        {
            Console.WriteLine($"+{x.RoomType}");
            if (x.RoomNumbers is not null)
            {
                foreach (var r in x.RoomNumbers)

                    Console.WriteLine($"\t--- {r} {(booked.Contains(r)?"Booked":"")}");
            }
        }

        Console.WriteLine("Empty");
        foreach (var x in All)
        {
            Console.WriteLine($"+{x.RoomType}");
            if (x.RoomNumbers is not null)
            {
                foreach (var r in x.RoomNumbers.Except(booked))
                    Console.WriteLine($"\t--- {r}");
            }
        }

        Console.WriteLine("Booked");
        foreach (var x in All)
        {
            Console.WriteLine($"+{x.RoomType}");
            if (x.RoomNumbers is not null)
            {
                foreach (var r in x.RoomNumbers.Intersect(booked))
                    Console.WriteLine($"\t--- {r}");
            }
        }

        Console.WriteLine("Summary");
        Console.WriteLine("RoomType,  Total, Booked, Free");
        foreach (var x in All)
        {
            Console.WriteLine($"{x.RoomType}, {x.RoomNumbers?.Count()}, {x.RoomNumbers?.Intersect(booked).Count()}, {x.RoomNumbers?.Except(booked).Count()}");
        }

    }
   
    //Ex7 in Model.cs

    public static void q8aSolution(HotelContext db)
    {
        //Exercise 8a: 
        var AllEmps = new List<Employee>() {new Employee (1,"Alice"),
        new Employee (2,"Bob",1), new Employee (3,"Claudia",1),
        new Employee (4,"Diana",2), new Employee (5,"Faris")};

        db.AddRange(AllEmps);
        db.SaveChanges();
    }

    public static void q8bSolution(HotelContext db)
    {
        //Exercise 8b: 
        var bob = db.employees.FirstOrDefault(x => x.Name == "Bob");
        if (bob != null)
        {
            //Uncommnet below lines if casecade behaviour is not configured

            //var sub = db.employees.Where(_ => _.BossID == bob.ID).ToList();
            //Nullable<int> nulli = null;
            //foreach (var e in sub) {
            //    e.BossID = nulli;
            //}
            
            //db.employees.UpdateRange(sub);
            db.employees.Remove(bob);
            db.SaveChanges();
        }


    }

}