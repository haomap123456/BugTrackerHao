﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using BugTracker.Models.Classes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketHistories> TicketHistories { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        [InverseProperty("Creator")]
        public virtual ICollection<Ticket> CreatedTickets { get; set; }

        [InverseProperty("Assignee")]
        public virtual ICollection<Ticket> AssignedTickets { get; set; }

       
        public ApplicationUser()
        {
            Projects = new HashSet<Project>();
            AssignedTickets = new HashSet<Ticket>();
            CreatedTickets = new HashSet<Ticket>();
            TicketHistories = new HashSet<TicketHistories>();
            TicketComments = new HashSet<TicketComment>();
            TicketAttachments = new HashSet<TicketAttachment>();
        }
       

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Project> Projects { get; set; }
        public System.Data.Entity.DbSet<Ticket> Tickets { get; set; }
        public System.Data.Entity.DbSet<TicketStatus> TicketStatus { get; set; }
        public System.Data.Entity.DbSet<TicketPriority> TicketPriorities { get; set; }
        public System.Data.Entity.DbSet<TicketType> TicketTypes { get; set; }

        public System.Data.Entity.DbSet<BugTracker.Models.TicketComment> TicketComments { get; set; }
        public System.Data.Entity.DbSet<BugTracker.Models.TicketHistories> TicketHistories { get; set; }
        public System.Data.Entity.DbSet<BugTracker.Models.TicketAttachment> TicketAttachments { get; set; }
        public System.Data.Entity.DbSet<BugTracker.Models.TicketNotification> TicketNotifications { get; set; }
    }
}