using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using QbcBackend.Botany.Validation;
using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Model.Botany;
using QbcBackend.Molecules.Parser;
using QbcBackend.Molecules.Repo;
using QbcBackend.Molecules.Services;
using QbcBackend.Molecules.Validations;
using Services.Molecules.Validations;

namespace QbcServices
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {

            if (serviceLifetime == ServiceLifetime.Scoped)
            {
                services.AddScoped<IBotanicalNameService, BotanicalNameService>();
                services.AddScoped<IBotanicalRankService, BotanicalRankService>();

                services.AddScoped<IMoleculeService, MoleculeService>();
                services.AddScoped<IGmsInputService, GmsInputService>();
                services.AddScoped<IChemicalProjectService, ChemicalProjectService>();
                services.AddScoped<IChemicalModelService, ChemicalModelService>();
                services.AddScoped<IChemicalCalculationService, ChemicalCalculationService>();
                services.AddScoped<IChemicalCalculationGroupService, ChemicalCalculationGroupService>();
                services.AddScoped<IChemicalBasissetService, ChemicalBasissetService>();
                services.AddScoped<ICalculationStatusService, CalculationStatusService>();

                services.AddScoped<IParserService, ParserService>();

            }
            else if (serviceLifetime == ServiceLifetime.Singleton)
            {
                services.AddSingleton<IBotanicalNameService, BotanicalNameService>();
                services.AddSingleton<IBotanicalRankService, BotanicalRankService>();

                services.AddSingleton<IMoleculeService, MoleculeService>();
                services.AddSingleton<IGmsInputService, GmsInputService>();
                services.AddSingleton<IChemicalProjectService, ChemicalProjectService>();
                services.AddSingleton<IChemicalModelService, ChemicalModelService>();
                services.AddSingleton<IChemicalCalculationService, ChemicalCalculationService>();
                services.AddSingleton<IChemicalCalculationGroupService, ChemicalCalculationGroupService>();
                services.AddSingleton<IChemicalBasissetService, ChemicalBasissetService>();
                services.AddSingleton<ICalculationStatusService, CalculationStatusService>();

                services.AddSingleton<IParserService, ParserService>();

            }
            else if (serviceLifetime == ServiceLifetime.Transient)
            {
                services.AddTransient<IBotanicalNameService, BotanicalNameService>();
                services.AddTransient<IBotanicalRankService, BotanicalRankService>();

                services.AddTransient<IMoleculeService, MoleculeService>();
                services.AddTransient<IGmsInputService, GmsInputService>();
                services.AddTransient<IChemicalProjectService, ChemicalProjectService>();
                services.AddTransient<IChemicalModelService, ChemicalModelService>();
                services.AddTransient<IChemicalCalculationService, ChemicalCalculationService>();
                services.AddTransient<IChemicalCalculationGroupService, ChemicalCalculationGroupService>();
                services.AddTransient<IChemicalBasissetService, ChemicalBasissetService>();
                services.AddTransient<ICalculationStatusService, CalculationStatusService>();

                services.AddTransient<IParserService, ParserService>();
            }

            return services;
        }

        public static IServiceCollection AddRepo(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {

            if (serviceLifetime == ServiceLifetime.Scoped)
            {
                services.AddScoped<IBotanicalNameRepo, BotanicalNameRepo>();
                services.AddScoped<IBotanicalNameTypeRepo, BotanicalNameTypeRepo>();

                services.AddScoped<IMoleculeRepository, MoleculeRepository>();
                services.AddScoped<IMoleculePropertyRepository, MoleculePropertyRepository>();
                services.AddScoped<IModelRepository, ModelRepository>();
                services.AddScoped<IConfigurationDataRepository, ConfigurationDataRepository>();
                services.AddScoped<ICategoryRepository, CategoryRepository>();
                services.AddScoped<ICalculationRepository, CalculationRepository>();
                services.AddScoped<ICalculationGroupRepository, CalculationGroupRepository>();
                services.AddScoped<IBondRepository, BondRepository>();
                services.AddScoped<IBondAtomRepository, BondAtomRepository>();
                services.AddScoped<IBasisSetRepository, BasisSetRepository>();
                services.AddScoped<IAtomRepository, AtomRepository>();
                services.AddScoped<IAtomOrbitalRepository, AtomOrbitalRepository>();
                services.AddScoped<IAtomInfoRepository, AtomInfoRepository>();

                services.AddScoped<IMoleculeElpotRepository, MoleculeElpotRepository>();


            }
            else if (serviceLifetime == ServiceLifetime.Singleton)
            {
                services.AddSingleton<IBotanicalNameRepo, BotanicalNameRepo>();
                services.AddSingleton<IBotanicalNameTypeRepo, BotanicalNameTypeRepo>();

                services.AddSingleton<IMoleculeRepository, MoleculeRepository>();
                services.AddSingleton<IMoleculePropertyRepository, MoleculePropertyRepository>();
                services.AddSingleton<IModelRepository, ModelRepository>();
                services.AddSingleton<IConfigurationDataRepository, ConfigurationDataRepository>();
                services.AddSingleton<ICategoryRepository, CategoryRepository>();
                services.AddSingleton<ICalculationRepository, CalculationRepository>();
                services.AddSingleton<ICalculationGroupRepository, CalculationGroupRepository>();
                services.AddSingleton<IBondRepository, BondRepository>();
                services.AddSingleton<IBondAtomRepository, BondAtomRepository>();
                services.AddSingleton<IBasisSetRepository, BasisSetRepository>();
                services.AddSingleton<IAtomRepository, AtomRepository>();
                services.AddSingleton<IAtomOrbitalRepository, AtomOrbitalRepository>();
                services.AddSingleton<IAtomInfoRepository, AtomInfoRepository>();

                services.AddSingleton<IMoleculeElpotRepository, MoleculeElpotRepository>();
            }
            else if (serviceLifetime == ServiceLifetime.Transient)
            {
                services.AddTransient<IBotanicalNameRepo, BotanicalNameRepo>();
                services.AddTransient<IBotanicalNameTypeRepo, BotanicalNameTypeRepo>();

                services.AddTransient<IMoleculeRepository, MoleculeRepository>();
                services.AddTransient<IMoleculePropertyRepository, MoleculePropertyRepository>();
                services.AddTransient<IModelRepository, ModelRepository>();
                services.AddTransient<IConfigurationDataRepository, ConfigurationDataRepository>();
                services.AddTransient<ICategoryRepository, CategoryRepository>();
                services.AddTransient<ICalculationRepository, CalculationRepository>();
                services.AddTransient<ICalculationGroupRepository, CalculationGroupRepository>();
                services.AddTransient<IBondRepository, BondRepository>();
                services.AddTransient<IBondAtomRepository, BondAtomRepository>();
                services.AddTransient<IBasisSetRepository, BasisSetRepository>();
                services.AddTransient<IAtomRepository, AtomRepository>();
                services.AddTransient<IAtomOrbitalRepository, AtomOrbitalRepository>();
                services.AddTransient<IAtomInfoRepository, AtomInfoRepository>();

                services.AddTransient<IMoleculeElpotRepository, MoleculeElpotRepository>();
            }

            return services;
        }

        public static IServiceCollection AddValidation(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (serviceLifetime == ServiceLifetime.Scoped)
            {
                services.AddScoped<IValidator<BotanicalRank>, BotanicalRankValidator>();
                services.AddScoped<IValidator<BotanicalNameInfo>, BotanicalNameInfoValidator>();

                services.AddScoped<IValidator<Molecule>, MoleculeValidator>();
                services.AddScoped<IValidator<MoleculeModel>, ModelValidator>();
                services.AddScoped<IValidator<Category>, CategoryValidator>();
                services.AddScoped<IValidator<Calculation>, CalculationValidator>();
                services.AddScoped<IValidator<CalculationGroup>, CalculationGroupValidator>();
                services.AddScoped<IValidator<Bond>, BondValidator>();
                services.AddScoped<IValidator<Atom>, AtomValidator>();

                services.AddScoped<IValidator<AtomOrbital>, AtomOrbitalValidator>();
                services.AddScoped<IValidator<BondAtom>, BondAtomValidator>();


            }
            else if (serviceLifetime == ServiceLifetime.Singleton)
            {
                services.AddSingleton<IValidator<BotanicalRank>, BotanicalRankValidator>();
                services.AddSingleton<IValidator<BotanicalNameInfo>, BotanicalNameInfoValidator>();


                services.AddSingleton<IValidator<Molecule>, MoleculeValidator>();
                services.AddSingleton<IValidator<MoleculeModel>, ModelValidator>();
                services.AddSingleton<IValidator<Category>, CategoryValidator>();
                services.AddSingleton<IValidator<Calculation>, CalculationValidator>();
                services.AddSingleton<IValidator<CalculationGroup>, CalculationGroupValidator>();
                services.AddSingleton<IValidator<Bond>, BondValidator>();
                services.AddSingleton<IValidator<Atom>, AtomValidator>();

                services.AddSingleton<IValidator<AtomOrbital>, AtomOrbitalValidator>();
                services.AddSingleton<IValidator<BondAtom>, BondAtomValidator>();
            }
            else if (serviceLifetime == ServiceLifetime.Transient)
            {
                services.AddTransient<IValidator<BotanicalRank>, BotanicalRankValidator>();
                services.AddTransient<IValidator<BotanicalNameInfo>, BotanicalNameInfoValidator>();


                services.AddTransient<IValidator<Molecule>, MoleculeValidator>();
                services.AddTransient<IValidator<MoleculeModel>, ModelValidator>();
                services.AddTransient<IValidator<Category>, CategoryValidator>();
                services.AddTransient<IValidator<Calculation>, CalculationValidator>();
                services.AddTransient<IValidator<CalculationGroup>, CalculationGroupValidator>();
                services.AddTransient<IValidator<Bond>, BondValidator>();
                services.AddTransient<IValidator<Atom>, AtomValidator>();

                services.AddTransient<IValidator<AtomOrbital>, AtomOrbitalValidator>();
                services.AddTransient<IValidator<BondAtom>, BondAtomValidator>();
            }

            return services;
        }
    }
}
