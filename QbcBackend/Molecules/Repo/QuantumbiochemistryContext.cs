using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;

namespace QbcBackend.Molecules.Repo
{
    public partial class QuantumbiochemistryContext : DbContext
    {
        public virtual DbSet<Atom> Atom { get; set; }
        public virtual DbSet<AtomInfo> AtomInfo { get; set; }
        public virtual DbSet<AtomOrbital> AtomOrbital { get; set; }
        public virtual DbSet<BasisSet> BasisSet { get; set; }
        public virtual DbSet<Bond> Bond { get; set; }
        public virtual DbSet<BondAtom> BondAtom { get; set; }
        public virtual DbSet<BotanicalName> BotanicalName { get; set; }
        public virtual DbSet<BotanicalNameAttribute> BotanicalNameAttribute { get; set; }
        public virtual DbSet<BotanicalNameType> BotanicalNameType { get; set; }
        public virtual DbSet<Calculation> Calculation { get; set; }
        public virtual DbSet<CalculationGroup> CalculationGroup { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<ConfigurationData> ConfigurationData { get; set; }
        public virtual DbSet<FtpConfig> FtpConfig { get; set; }
        public virtual DbSet<Molecule> Molecule { get; set; }
        public virtual DbSet<MoleculeElPot> MoleculeElPot { get; set; }
        public virtual DbSet<MoleculeModel> MoleculeModel { get; set; }
        public virtual DbSet<MoleculeProperty> MoleculeProperty { get; set; }


        public QuantumbiochemistryContext(DbContextOptions<QuantumbiochemistryContext> options)
            :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atom>(entity =>
            {
                entity.HasIndex(e => e.AppId);

                entity.HasIndex(e => e.MoleculeId);

                entity.HasIndex(e => e.Number);

                entity.HasIndex(e => e.Symbol);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.ChelpGcharge)
                    .HasColumnName("CHelpGCharge")
                    .HasColumnType("decimal(18, 7)");

                entity.Property(e => e.ConnollyCharge).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.GeoDiscCharge).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.LowdinPopulation).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.LowdinPopulationAcid).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.LowdinPopulationBase).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.MoleculeId).HasColumnName("MoleculeID");

                entity.Property(e => e.MullikenPopulation).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.MullikenPopulationAcid).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.MullikenPopulationBase).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.PosX).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.PosY).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.PosZ).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.Radius).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Molecule)
                    .WithMany(p => p.Atom)
                    .HasForeignKey(d => d.MoleculeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Atom_Molecule");
            });

            modelBuilder.Entity<AtomInfo>(entity =>
            {
                entity.HasIndex(e => e.AppId);

                entity.HasIndex(e => e.AtomNumber);

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => e.Symbol);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.AtomMass).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.AtomRadius).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.ElectroNegativity).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.ElectronAffinity).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<AtomOrbital>(entity =>
            {
                entity.HasIndex(e => e.AppId);

                entity.HasIndex(e => e.AtomId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.AtomId).HasColumnName("AtomID");

                entity.Property(e => e.LowdinPopulation).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.LowdinPopulationAcid).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.LowdinPopulationBase).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.MullikenPopulation).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.MullikenPopulationAcid).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.MullikenPopulationBase).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Atom)
                    .WithMany(p => p.AtomOrbital)
                    .HasForeignKey(d => d.AtomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AtomOrbital_Atom");
            });

            modelBuilder.Entity<BasisSet>(entity =>
            {
                entity.HasIndex(e => e.AppId);

                entity.HasIndex(e => e.Code);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GmsInput)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Bond>(entity =>
            {
                entity.HasIndex(e => e.MoleculeId);

                entity.HasIndex(e => new { e.Atom1Position, e.Atom2Position })
                    .HasName("IX_Bond_Position");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.BondOrder).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.BondOrderAcid).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.BondOrderBase).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.Distance).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.MoleculeId).HasColumnName("MoleculeID");

                entity.Property(e => e.OverlapPopulation).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.OverlapPopulationAcid).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.OverlapPopulationBase).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Molecule)
                    .WithMany(p => p.Bond)
                    .HasForeignKey(d => d.MoleculeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bond_Molecule");
            });

            modelBuilder.Entity<BondAtom>(entity =>
            {
                entity.HasIndex(e => e.AppId);

                entity.HasIndex(e => e.AtomId);

                entity.HasIndex(e => e.BondId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.AtomId).HasColumnName("AtomID");

                entity.Property(e => e.BondId).HasColumnName("BondID");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Atom)
                    .WithMany(p => p.BondAtom)
                    .HasForeignKey(d => d.AtomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BondAtom_Atom");

                entity.HasOne(d => d.Bond)
                    .WithMany(p => p.BondAtom)
                    .HasForeignKey(d => d.BondId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BondAtom_Bond");
            });

            modelBuilder.Entity<BotanicalName>(entity =>
            {
                entity.Property(e => e.BotanicalNameId).HasColumnName("BotanicalNameID");

                entity.Property(e => e.BotanicalNameTypeId).HasColumnName("BotanicalNameTypeID");

                entity.Property(e => e.Description).HasMaxLength(2500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.BotanicalNameNavigation)
                    .WithMany(p => p.InverseBotanicalNameNavigation)
                    .HasForeignKey(d => d.BotanicalNameId);

                entity.HasOne(d => d.BotanicalNameType)
                    .WithMany(p => p.BotanicalName)
                    .HasForeignKey(d => d.BotanicalNameTypeId);
            });

            modelBuilder.Entity<BotanicalNameAttribute>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.HasOne(d => d.BotanicalName)
                    .WithMany(p => p.BotanicalNameAttribute)
                    .HasForeignKey(d => d.BotanicalNameId)
                    .HasConstraintName("FK_BotanicalNameAttribute_BotanicalName");

                entity.HasOne(d => d.MoleculeModel)
                    .WithMany(p => p.BotanicalNameAttribute)
                    .HasForeignKey(d => d.MoleculeModelId)
                    .HasConstraintName("FK_BotanicalNameAttribute_MoleculeModel");
            });

            modelBuilder.Entity<BotanicalNameType>(entity =>
            {
                entity.Property(e => e.BotanicalNameTypeId).HasColumnName("BotanicalNameTypeID");

                entity.Property(e => e.Description).HasMaxLength(2500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.BotanicalNameTypeNavigation)
                    .WithMany(p => p.InverseBotanicalNameTypeNavigation)
                    .HasForeignKey(d => d.BotanicalNameTypeId);
            });

            modelBuilder.Entity<Calculation>(entity =>
            {
                entity.HasIndex(e => e.AppId);

                entity.HasIndex(e => e.BasisSetId);

                entity.HasIndex(e => e.Code);

                entity.HasIndex(e => e.ModelId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.BasisSetId).HasColumnName("BasisSetID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GmsInput).IsUnicode(false);

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.BasisSet)
                    .WithMany(p => p.Calculation)
                    .HasForeignKey(d => d.BasisSetId)
                    .HasConstraintName("FK_Calculation_BasisSet");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Calculation)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Calculation_Model");
            });

            modelBuilder.Entity<CalculationGroup>(entity =>
            {
                entity.HasIndex(e => e.AppId);

                entity.HasIndex(e => e.CalcId);

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => e.ParentCalcId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.CalcId).HasColumnName("CalcID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentCalcId).HasColumnName("ParentCalcID");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Calc)
                    .WithMany(p => p.CalculationGroup)
                    .HasForeignKey(d => d.CalcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalculationGroup_Calculation");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.AppId);

                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.Code);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.InverseCategoryNavigation)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<ConfigurationData>(entity =>
            {
                entity.HasIndex(e => e.Name);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.GmsArchiveDir)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GmsInputDir)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GmsOutputDir)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Host)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UserName)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FtpConfig>(entity =>
            {
                entity.HasIndex(e => e.Name);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ArchiveFileDir)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Host)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InputFileDir)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OutputFileDir)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Molecule>(entity =>
            {
                entity.HasIndex(e => e.AppId);

                entity.HasIndex(e => e.ModelId);

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => e.ParentCalculationId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentCalculationId).HasColumnName("ParentCalculationID");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Molecule)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Molecule_Model");

                entity.HasOne(d => d.ParentCalculation)
                    .WithMany(p => p.Molecule)
                    .HasForeignKey(d => d.ParentCalculationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Molecule_Calculation");
            });

            modelBuilder.Entity<MoleculeElPot>(entity =>
            {
                entity.HasIndex(e => e.MoleculeId)
                    .HasName("IX_MoleculeElPot");

                entity.HasIndex(e => e.Type)
                    .HasName("IX_MoleculeElPot_1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Electronic).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.MoleculeId).HasColumnName("MoleculeID");

                entity.Property(e => e.Nuclear).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.PosX).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.PosY).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.PosZ).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Total).HasColumnType("decimal(18, 7)");

                entity.HasOne(d => d.Molecule)
                    .WithMany(p => p.MoleculeElPot)
                    .HasForeignKey(d => d.MoleculeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoleculeElPot_Molecule");
            });

            modelBuilder.Entity<MoleculeModel>(entity =>
            {
                entity.HasIndex(e => e.AppId)
                    .HasName("IX_Model_AppID");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_Model_CategoryID");

                entity.HasIndex(e => e.Code)
                    .HasName("IX_Model_Code");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentGeometry).IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.InitialGeometry).IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.MoleculeModel)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Model_Category");
            });

            modelBuilder.Entity<MoleculeProperty>(entity =>
            {
                entity.HasIndex(e => e.AppId);

                entity.HasIndex(e => e.Code);

                entity.HasIndex(e => e.MoleculeId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.MoleculeId).HasColumnName("MoleculeID");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Molecule)
                    .WithMany(p => p.MoleculeProperty)
                    .HasForeignKey(d => d.MoleculeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoleculeProperty_Molecule");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
