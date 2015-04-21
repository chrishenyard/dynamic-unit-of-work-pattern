namespace Region.Repository {
	using System.Data.Entity;

	public partial class RegionContext : DbContext {
		public RegionContext()
			: base("name=RegionContext") {
		}

		public static RegionContext Create() {
			return new RegionContext();
		}

		public virtual DbSet<Zipcode> Zipcodes { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Entity<Zipcode>()
				.Property(e => e.State)
				.IsFixedLength();

			modelBuilder.Entity<Zipcode>()
				.Property(e => e.Latitude)
				.HasPrecision(9, 6);

			modelBuilder.Entity<Zipcode>()
				.Property(e => e.Longitude)
				.HasPrecision(9, 6);

			modelBuilder.Entity<Zipcode>()
				.Property(e => e.Country)
				.IsFixedLength();
		}
	}
}
