﻿using System.Linq;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates
{
    public class EntityClassTemplate : CodeTemplateBase
    {
        private readonly Entity _entity;

        public EntityClassTemplate(Entity entity, GeneratorOptions options) : base(options)
        {
            _entity = entity;
        }

        public override string WriteCode()
        {
            CodeBuilder.Clear();

            CodeBuilder.AppendLine("using System;");
            CodeBuilder.AppendLine("using System.Collections.Generic;");
            CodeBuilder.AppendLine();

            CodeBuilder.AppendLine($"namespace {_entity.EntityNamespace}");
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                GenerateClass();
            }

            CodeBuilder.AppendLine("}");

            return CodeBuilder.ToString();
        }

        private void GenerateClass()
        {
            var entityClass = _entity.EntityClass.ToSafeName();

            if (Options.Data.Entity.Document)
            {
                CodeBuilder.AppendLine("/// <summary>");
                CodeBuilder.AppendLine($"/// Entity class representing data for table '{_entity.TableName}'.");
                CodeBuilder.AppendLine("/// </summary>");
            }

            CodeBuilder.AppendLine($"public partial class {entityClass}");

            if (_entity.EntityBaseClass.HasValue())
            {
                var entityBaseClass = _entity.EntityBaseClass.ToSafeName();
                using (CodeBuilder.Indent())
                    CodeBuilder.AppendLine($": {entityBaseClass}");
            }

            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                GenerateConstructor();

                GenerateProperties();
                GenerateRelationshipProperties();
            }

            CodeBuilder.AppendLine("}");

        }

        private void GenerateConstructor()
        {
            var relationships = _entity.Relationships
                .Where(r => r.Cardinality == Cardinality.Many)
                .OrderBy(r => r.PropertyName)
                .ToList();

            var entityClass = _entity.EntityClass.ToSafeName();

            if (Options.Data.Entity.Document)
            {
                CodeBuilder.AppendLine("/// <summary>");
                CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{entityClass}\"/> class.");
                CodeBuilder.AppendLine("/// </summary>");
            }

            CodeBuilder.AppendLine($"public {entityClass}()");
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                foreach (var relationship in relationships)
                {
                    var propertyName = relationship.PropertyName.ToSafeName();
                    var primaryName = relationship.PrimaryEntity.EntityClass.ToSafeName();

                    CodeBuilder.AppendLine($"{propertyName} = new HashSet<{primaryName}>();");
                }
            }

            CodeBuilder.AppendLine("}");
            CodeBuilder.AppendLine();
        }

        private void GenerateProperties()
        {
            foreach (var property in _entity.Properties)
            {
                var propertyType = property.SystemType.ToNullableType(property.IsNullable == true);
                var propertyName = property.PropertyName.ToSafeName();

                if (Options.Data.Entity.Document)
                {
                    CodeBuilder.AppendLine("/// <summary>");
                    CodeBuilder.AppendLine($"/// Gets or sets the property value representing column '{property.ColumnName}'.");
                    CodeBuilder.AppendLine("/// </summary>");
                    CodeBuilder.AppendLine("/// <value>");
                    CodeBuilder.AppendLine($"/// The property value representing column '{property.ColumnName}'.");
                    CodeBuilder.AppendLine("/// </value>");
                }

                CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }}");
                if (!IsLastIndex(_entity.Properties, property))
                {
                    CodeBuilder.AppendLine();
                }
            }

            if (_entity.Relationships.Any())
            {
                CodeBuilder.AppendLine();
            }
        }

        private void GenerateRelationshipProperties()
        {
            foreach (var relationship in _entity.Relationships.OrderBy(r => r.PropertyName))
            {
                var propertyName = relationship.PropertyName.ToSafeName();
                var primaryName = relationship.PrimaryEntity.EntityClass.ToSafeName();

                if (relationship.Cardinality == Cardinality.Many)
                {
                    if (Options.Data.Entity.Document)
                    {
                        CodeBuilder.AppendLine("/// <summary>");
                        CodeBuilder.AppendLine($"/// Gets or sets the navigation collection for entity <see cref=\"{primaryName}\" />.");
                        CodeBuilder.AppendLine("/// </summary>");
                        CodeBuilder.AppendLine("/// <value>");
                        CodeBuilder.AppendLine($"/// The the navigation collection for entity <see cref=\"{primaryName}\" />.");
                        CodeBuilder.AppendLine("/// </value>");
                    }

                    CodeBuilder.AppendLine($"public virtual ICollection<{primaryName}> {propertyName} {{ get; set; }}");
                    CodeBuilder.AppendLine();
                }
                else
                {
                    if (Options.Data.Entity.Document)
                    {
                        CodeBuilder.AppendLine("/// <summary>");
                        CodeBuilder.AppendLine($"/// Gets or sets the navigation property for entity <see cref=\"{primaryName}\" />.");
                        CodeBuilder.AppendLine("/// </summary>");
                        CodeBuilder.AppendLine("/// <value>");
                        CodeBuilder.AppendLine($"/// The the navigation property for entity <see cref=\"{primaryName}\" />.");
                        CodeBuilder.AppendLine("/// </value>");

                        foreach (var property in relationship.Properties)
                            CodeBuilder.AppendLine($"/// <seealso cref=\"{property.PropertyName}\" />");
                    }

                    CodeBuilder.AppendLine($"public virtual {primaryName} {propertyName} {{ get; set; }}");
                    if (!IsLastIndex(_entity.Relationships, relationship))
                    {
                        CodeBuilder.AppendLine();
                    }
                }
            }
        }
    }
}