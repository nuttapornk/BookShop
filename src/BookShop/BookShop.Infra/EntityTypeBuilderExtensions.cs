using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace BookShop.Infra
{
    public static class EntityTypeBuilderExtensions
    {
        public static void Config<T>(this EntityTypeBuilder<T> modelBuilder, params Action<EntityTypeBuilder<T>>[] builders) where T : class
        {
            builders
                .ToList()
                .ForEach(builder => builder(modelBuilder));
        }
    }
}
