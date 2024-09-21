using Folkin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Folkin.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Kort_og_tag>().HasKey(kt => new
            {
                kt.KortId,
                kt.TagId

            });

            modelBuilder.Entity<Kort_og_Type>().HasKey(kty => new
            {
                kty.KortId,
                kty.TypeId

            });
            //Defind Kort_og_tag join model 
            modelBuilder.Entity<Kort_og_tag>().HasOne(k => k.Korts).WithMany(kt => kt.Korts_og_tags).HasForeignKey(k => k.KortId);
            modelBuilder.Entity<Kort_og_tag>().HasOne(k => k.Tags).WithMany(kt => kt.Korts_og_tags).HasForeignKey(k => k.TagId);

            //Defind Kort_og-type join model 
            modelBuilder.Entity<Kort_og_Type>().HasOne(ky => ky.Kort).WithMany(kty => kty.Korts_og_Types).HasForeignKey(k => k.KortId);
            modelBuilder.Entity<Kort_og_Type>().HasOne(ky => ky.Type).WithMany(kty => kty.Korts_og_Types).HasForeignKey(k => k.TypeId);

            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Kort>().HasData(
            // new Kort()
            // {
            //     Id = 1,
            //     Titel = "MEELEE",
            //     Beskivelse = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
            //     " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s,",
            //     Abstract = false,
            //     SillhouetteId = 1,
            // },
            // new Kort()
            // {
            //     Id = 2,
            //     Titel = "RANGED",
            //     Beskivelse = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
            //     " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s,",
            //     Abstract = true,
            //     SillhouetteId = 2,

            // });
            modelBuilder.Entity<Sillhouette>().HasData(
                new Sillhouette()
                {
                    Id = 1,
                    Titel = "bow",
                    IconURL = "bow.png",
                },
                new Sillhouette()
                {
                    Id = 2,
                    Titel = "sword",
                    IconURL = "sword.png"
                },
                new Sillhouette()
                {
                    Id = 3,
                    Titel = "potion",
                    IconURL = "potion.png"
                },
                new Sillhouette()
                {
                    Id = 4,
                    Titel = "necklace",
                    IconURL = "necklace.png"
                }
                );
            modelBuilder.Entity<Type>().HasData(
             new Type()
             {
                 Id = 1,
                 Titel = "Niche skill",
             },
             new Type()
             {
                 Id = 2,
                 Titel = "Skill",
             },
             new Type()
             {
                 Id = 3,
                 Titel = "fantasy",
             },
                new Type()
                {
                    Id = 4,
                    Titel = "Item tag",
                },
                new Type()
                {
                    Id = 5,
                    Titel = "Artifact item tag",
                },
                new Type()
                {
                    Id = 6,
                    Titel = "XX Tag",
                }

             );
            modelBuilder.Entity<Tag>().HasData(
             new Tag()
             {
                 Id = 1,
                 Titel = "Knife",
                 Type = "domaine",
                 IconURL = "Asset 690logo.svg",
             },
             new Tag()
             {
                 Id = 2,
                 Titel = "sword",
                 Type = "domaine",
                 IconURL = "Asset 689logo.svg",
             },
             new Tag()
             {
                 Id = 3,
                 Titel = "Bow",
                 Type = "domaine",
                 IconURL = "Asset 691logo.svg",
             },
             new Tag()
             {
                 Id = 4,
                 Titel = "Long Bow",
                 Type = "domaine",
                 IconURL = "Asset 692logo.svg",
             },
             new Tag()
             {
                 Id = 5,
                 Titel = "speech",
                 IconURL = "Asset 693logo.svg",
             },
             new Tag()
             {
                 Id = 6,
                 Titel = "health_6",
                 IconURL = "Asset 701logo.svg",
             }

             );

            //modelBuilder.Entity<Kort_og_tag>().HasData(
            // new Kort_og_tag()
            //{
            //    KortId = 1,
            //    TagId = 1,

            //}, 
            // new Kort_og_tag()
            //{
            //    KortId = 1,
            //    TagId = 2,

            //},  
            // new Kort_og_tag()
            //{
            //    KortId = 1,
            //    TagId = 3,

            //},
            // new Kort_og_tag()
            //{
            //    KortId = 2,
            //    TagId = 4,

            //}

            // );
            //modelBuilder.Entity<Kort_og_Type>().HasData(
            // new Kort_og_Type()
            //{
            //    KortId = 1,
            //    TypeId = 1,

            //},
            // new Kort_og_Type()
            //{
            //    KortId = 1,
            //    TypeId = 3,

            //},
            // new Kort_og_Type()
            //{
            //    KortId = 2,
            //    TypeId = 4,

            //},
            // new Kort_og_Type()
            //{
            //    KortId = 2,
            //    TypeId = 5,
            //}

            // );


        }
        public DbSet<Kort> Korts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Kort_og_tag> Korts_og_tags { get; set; }
        public DbSet<Kort_og_Type> Korts_og_Types { get; set; }
        public DbSet<Sillhouette> Sillhouettes { get; set; }




        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{


        //    //    base.OnModelCreating(modelBuilder);
        //    //    modelBuilder.Entity<Tag>().HasData(
        //    //     new Tag()
        //    //     {            
        //    //         id=1,
        //    //         Titel= "tagtest",
        //    //         IconURL="tagtest",
        //    //         Type = "typ"
        //    //     });      
        //    //    base.OnModelCreating(modelBuilder);
        //    //    modelBuilder.Entity<Sillhouette>().HasData(
        //    //     new Sillhouette()
        //    //     {            
        //    //         id=1,
        //    //         Titel= "tagtest",
        //    //         IconURL="tagtest"
        //    //     });      
        //    //    base.OnModelCreating(modelBuilder);
        //    //    modelBuilder.Entity<Type>().HasData(
        //    //     new Type()
        //    //     {            
        //    //         id=1,
        //    //         Titel= "tagtest",
        //    //         IconURL="tagtest"
        //    //     });
        //}
    }

}
