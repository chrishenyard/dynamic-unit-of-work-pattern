namespace Region.Repository {
	using System.ComponentModel.DataAnnotations;

	public partial class Zipcode
    {
        [Key]
        [StringLength(10)]
        public string Zip { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        public string PrimaryCity { get; set; }

        [StringLength(500)]
        public string AcceptableCities { get; set; }

        [StringLength(500)]
        public string UnacceptableCities { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [StringLength(50)]
        public string County { get; set; }

        [StringLength(50)]
        public string Timezone { get; set; }

        [StringLength(50)]
        public string AreaCodes { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        [StringLength(100)]
        public string WorldRegion { get; set; }

        [Required]
        [StringLength(2)]
        public string Country { get; set; }

        public bool Decommissioned { get; set; }

        public int? EstimatedPopulation { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }
    }
}
