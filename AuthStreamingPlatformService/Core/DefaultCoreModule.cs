using Autofac;
using TechDaily.Core.Abstractions.ChatGPT;
using TechDaily.Core.Abstractions.Repositories;
using TechDaily.Core.Abstractions.Services;
using TechDaily.Core.ChatGPT;
using TechDaily.Core.Repositories;
using TechDaily.Core.Services;
using TechDaily.Entities;

namespace TechDaily.Core;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<QuestionsService>().As<IQuestionsService>();
        builder.RegisterType<QuestionsRepository>().As<IQuestionsRepository>();

        builder.RegisterType<CategoriesService>().As<ICategoriesService>();
        builder.RegisterType<CategoriesRepository>().As<ICategoriesRepository>();
        
        builder.RegisterType<SubCategoriesService>().As<ISubCategoriesService>();
        builder.RegisterType<SubCategoriesRepository>().As<ISubCategoriesRepository>();
        
        builder.RegisterType<QuestionsSubCategoriesRepository>().As<IQuestionsSubCategoriesRepository>();
        
        builder.RegisterType<GenerateService>().As<IGenerateService>();
        
        builder.RegisterType<ChatGptModel>().As<IChatGptModel>();
    }
}