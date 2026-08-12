using Bookstore.Domain.Addresses;
using Bookstore.Domain.Books;
using Bookstore.Domain.Carts;
using Bookstore.Domain.Customers;
using Bookstore.Domain.Offers;
using Bookstore.Domain.Orders;
using Bookstore.Domain.ReferenceData;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Address> Address { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<Offer> Offer { get; set; }

        public DbSet<ReferenceDataItem> ReferenceData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasIndex(x => x.Sub).IsUnique();

            modelBuilder.Entity<Book>().HasOne(x => x.Publisher).WithMany().HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>().HasOne(x => x.BookType).WithMany().HasForeignKey(x => x.BookTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>().HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>().HasOne(x => x.Condition).WithMany().HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>().HasOne(x => x.Publisher).WithMany().HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Offer>().HasOne(x => x.BookType).WithMany().HasForeignKey(x => x.BookTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Offer>().HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Offer>().HasOne(x => x.Condition).WithMany().HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>().ToTable("orders", "bobsusedbookstore_dbo");
            modelBuilder.Entity<Order>().HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);

            // Address
            modelBuilder.Entity<Address>().ToTable("address", "bobsusedbookstore_dbo");
            modelBuilder.Entity<Address>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<Address>().Property(e => e.AddressLine1).HasColumnName("addressline1");
            modelBuilder.Entity<Address>().Property(e => e.AddressLine2).HasColumnName("addressline2");
            modelBuilder.Entity<Address>().Property(e => e.City).HasColumnName("city");
            modelBuilder.Entity<Address>().Property(e => e.State).HasColumnName("state");
            modelBuilder.Entity<Address>().Property(e => e.Country).HasColumnName("country");
            modelBuilder.Entity<Address>().Property(e => e.ZipCode).HasColumnName("zipcode");
            modelBuilder.Entity<Address>().Property(e => e.CustomerId).HasColumnName("customerid");
            modelBuilder.Entity<Address>().Property(e => e.IsActive).HasColumnName("isactive");
            modelBuilder.Entity<Address>().Property(e => e.CreatedBy).HasColumnName("createdby");
            modelBuilder.Entity<Address>().Property(e => e.CreatedOn).HasColumnName("createdon");
            modelBuilder.Entity<Address>().Property(e => e.UpdatedOn).HasColumnName("updatedon");

            // Book
            modelBuilder.Entity<Book>().ToTable("book", "bobsusedbookstore_dbo");
            modelBuilder.Entity<Book>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<Book>().Property(e => e.Name).HasColumnName("name");
            modelBuilder.Entity<Book>().Property(e => e.Author).HasColumnName("author");
            modelBuilder.Entity<Book>().Property(e => e.Year).HasColumnName("year");
            modelBuilder.Entity<Book>().Property(e => e.ISBN).HasColumnName("isbn");
            modelBuilder.Entity<Book>().Property(e => e.PublisherId).HasColumnName("publisherid");
            modelBuilder.Entity<Book>().Property(e => e.BookTypeId).HasColumnName("booktypeid");
            modelBuilder.Entity<Book>().Property(e => e.GenreId).HasColumnName("genreid");
            modelBuilder.Entity<Book>().Property(e => e.ConditionId).HasColumnName("conditionid");
            modelBuilder.Entity<Book>().Property(e => e.CoverImageUrl).HasColumnName("coverimageurl");
            modelBuilder.Entity<Book>().Property(e => e.Summary).HasColumnName("summary");
            modelBuilder.Entity<Book>().Property(e => e.Price).HasColumnName("price");
            modelBuilder.Entity<Book>().Property(e => e.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<Book>().Property(e => e.CreatedBy).HasColumnName("createdby");
            modelBuilder.Entity<Book>().Property(e => e.CreatedOn).HasColumnName("createdon");
            modelBuilder.Entity<Book>().Property(e => e.UpdatedOn).HasColumnName("updatedon");

            // Customer
            modelBuilder.Entity<Customer>().ToTable("customer", "bobsusedbookstore_dbo");
            modelBuilder.Entity<Customer>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<Customer>().Property(e => e.Sub).HasColumnName("sub");
            modelBuilder.Entity<Customer>().Property(e => e.Username).HasColumnName("username");
            modelBuilder.Entity<Customer>().Property(e => e.FirstName).HasColumnName("firstname");
            modelBuilder.Entity<Customer>().Property(e => e.LastName).HasColumnName("lastname");
            modelBuilder.Entity<Customer>().Property(e => e.Email).HasColumnName("email");
            modelBuilder.Entity<Customer>().Property(e => e.DateOfBirth).HasColumnName("dateofbirth");
            modelBuilder.Entity<Customer>().Property(e => e.Phone).HasColumnName("phone");
            modelBuilder.Entity<Customer>().Property(e => e.CreatedBy).HasColumnName("createdby");
            modelBuilder.Entity<Customer>().Property(e => e.CreatedOn).HasColumnName("createdon");
            modelBuilder.Entity<Customer>().Property(e => e.UpdatedOn).HasColumnName("updatedon");

            // Order
            modelBuilder.Entity<Order>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<Order>().Property(e => e.CustomerId).HasColumnName("customerid");
            modelBuilder.Entity<Order>().Property(e => e.AddressId).HasColumnName("addressid");
            modelBuilder.Entity<Order>().Property(e => e.DeliveryDate).HasColumnName("deliverydate");
            modelBuilder.Entity<Order>().Property(e => e.OrderStatus).HasColumnName("orderstatus");
            modelBuilder.Entity<Order>().Property(e => e.CreatedBy).HasColumnName("createdby");
            modelBuilder.Entity<Order>().Property(e => e.CreatedOn).HasColumnName("createdon");
            modelBuilder.Entity<Order>().Property(e => e.UpdatedOn).HasColumnName("updatedon");

            // OrderItem
            modelBuilder.Entity<OrderItem>().ToTable("orderitem", "bobsusedbookstore_dbo");
            modelBuilder.Entity<OrderItem>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<OrderItem>().Property(e => e.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<OrderItem>().Property(e => e.BookId).HasColumnName("bookid");
            modelBuilder.Entity<OrderItem>().Property(e => e.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<OrderItem>().Property(e => e.CreatedBy).HasColumnName("createdby");
            modelBuilder.Entity<OrderItem>().Property(e => e.CreatedOn).HasColumnName("createdon");
            modelBuilder.Entity<OrderItem>().Property(e => e.UpdatedOn).HasColumnName("updatedon");

            // Offer
            modelBuilder.Entity<Offer>().ToTable("offer", "bobsusedbookstore_dbo");
            modelBuilder.Entity<Offer>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<Offer>().Property(e => e.Author).HasColumnName("author");
            modelBuilder.Entity<Offer>().Property(e => e.ISBN).HasColumnName("isbn");
            modelBuilder.Entity<Offer>().Property(e => e.BookName).HasColumnName("bookname");
            modelBuilder.Entity<Offer>().Property(e => e.FrontUrl).HasColumnName("fronturl");
            modelBuilder.Entity<Offer>().Property(e => e.GenreId).HasColumnName("genreid");
            modelBuilder.Entity<Offer>().Property(e => e.ConditionId).HasColumnName("conditionid");
            modelBuilder.Entity<Offer>().Property(e => e.PublisherId).HasColumnName("publisherid");
            modelBuilder.Entity<Offer>().Property(e => e.BookTypeId).HasColumnName("booktypeid");
            modelBuilder.Entity<Offer>().Property(e => e.Summary).HasColumnName("summary");
            modelBuilder.Entity<Offer>().Property(e => e.OfferStatus).HasColumnName("offerstatus");
            modelBuilder.Entity<Offer>().Property(e => e.Comment).HasColumnName("comment");
            modelBuilder.Entity<Offer>().Property(e => e.CustomerId).HasColumnName("customerid");
            modelBuilder.Entity<Offer>().Property(e => e.BookPrice).HasColumnName("bookprice");
            modelBuilder.Entity<Offer>().Property(e => e.CreatedBy).HasColumnName("createdby");
            modelBuilder.Entity<Offer>().Property(e => e.CreatedOn).HasColumnName("createdon");
            modelBuilder.Entity<Offer>().Property(e => e.UpdatedOn).HasColumnName("updatedon");

            // ReferenceData
            modelBuilder.Entity<ReferenceDataItem>().ToTable("referencedata", "bobsusedbookstore_dbo");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.DataType).HasColumnName("datatype");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.Text).HasColumnName("text");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.CreatedBy).HasColumnName("createdby");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.CreatedOn).HasColumnName("createdon");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.UpdatedOn).HasColumnName("updatedon");

            // ShoppingCart
            modelBuilder.Entity<ShoppingCart>().ToTable("shoppingcart", "bobsusedbookstore_dbo");
            modelBuilder.Entity<ShoppingCart>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<ShoppingCart>().Property(e => e.CorrelationId).HasColumnName("correlationid");
            modelBuilder.Entity<ShoppingCart>().Property(e => e.CreatedBy).HasColumnName("createdby");
            modelBuilder.Entity<ShoppingCart>().Property(e => e.CreatedOn).HasColumnName("createdon");
            modelBuilder.Entity<ShoppingCart>().Property(e => e.UpdatedOn).HasColumnName("updatedon");

            // ShoppingCartItem
            modelBuilder.Entity<ShoppingCartItem>().ToTable("shoppingcartitem", "bobsusedbookstore_dbo");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.ShoppingCartId).HasColumnName("shoppingcartid");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.BookId).HasColumnName("bookid");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.WantToBuy).HasColumnName("wanttobuy");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.CreatedBy).HasColumnName("createdby");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.CreatedOn).HasColumnName("createdon");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.UpdatedOn).HasColumnName("updatedon");

            PopulateDatabase(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
