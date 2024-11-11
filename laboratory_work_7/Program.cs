using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml;


namespace laboratory_work_7
{
    internal class Program
    {

        static void Main(string[] args)
        {
            XMLDiagramGenerator.GenerateDiagram("class_library_laboratory_work_7.dll", "XMLDiagram.xml");
        }
    }

    class XMLDiagramGenerator
    {
        public static void GenerateDiagram(string dllPath, string xmlFilePath)
        {
            Assembly assembly = Assembly.LoadFrom(dllPath);
            Type[] types = assembly.GetTypes();

            using (XmlWriter writer = XmlWriter.Create(xmlFilePath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Assembly");
                writer.WriteAttributeString("Name", assembly.GetName().Name);

                foreach (Type type in types)
                {
                    writer.WriteStartElement("Type");
                    writer.WriteAttributeString("Name", type.Name);
                    writer.WriteAttributeString("Namespace", type.Namespace);

                    writer.WriteStartElement("Attributes");
                    foreach (Attribute attribute in type.GetCustomAttributes())
                    {
                        writer.WriteStartElement("Attribute");
                        writer.WriteAttributeString("Name", attribute.GetType().Name);
                        writer.WriteEndElement(); // Attribute
                    }
                    writer.WriteEndElement(); // Attributes

                    writer.WriteStartElement("Fields");
                    foreach (FieldInfo field in type.GetFields())
                    {
                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("Type", field.FieldType.Name);
                        writer.WriteAttributeString("Name", field.Name);
                        writer.WriteAttributeString("IsPublic", field.IsPublic.ToString());
                        writer.WriteAttributeString("IsPrivate", field.IsPrivate.ToString());
                        writer.WriteAttributeString("IsStatic", field.IsStatic.ToString());
                        writer.WriteEndElement(); // Field
                    }
                    writer.WriteEndElement(); // Fields

                    writer.WriteStartElement("Properties");
                    foreach (PropertyInfo property in type.GetProperties())
                    {
                        writer.WriteStartElement("Property");
                        writer.WriteAttributeString("Type", property.PropertyType.Name);
                        writer.WriteAttributeString("Name", property.Name);
                        writer.WriteAttributeString("IsPublic", property.GetMethod.IsPublic.ToString());
                        writer.WriteAttributeString("IsPrivate", property.GetMethod.IsPrivate.ToString());
                        writer.WriteAttributeString("IsStatic", property.GetMethod.IsStatic.ToString());
                        writer.WriteAttributeString("CanRead", property.CanRead.ToString());
                        writer.WriteAttributeString("CanWrite", property.CanWrite.ToString());
                        writer.WriteEndElement(); // Property
                    }
                    writer.WriteEndElement(); // Properties

                    writer.WriteStartElement("Constructors");
                    foreach (ConstructorInfo constructor in type.GetConstructors())
                    {
                        writer.WriteStartElement("Constructor");
                        writer.WriteAttributeString("Name", constructor.Name);
                        writer.WriteAttributeString("IsPublic", constructor.IsPublic.ToString());
                        writer.WriteAttributeString("IsPrivate", constructor.IsPrivate.ToString());
                        writer.WriteAttributeString("IsStatic", constructor.IsStatic.ToString());
                        
                        writer.WriteStartElement("Parameters");
                        foreach (ParameterInfo parameter in constructor.GetParameters())
                        {
                            writer.WriteStartElement("Parameter");
                            writer.WriteAttributeString("Type", parameter.ParameterType.Name);
                            writer.WriteAttributeString("Name", parameter.Name);
                            writer.WriteAttributeString("IsIn", parameter.IsIn.ToString());
                            writer.WriteAttributeString("IsOut", parameter.IsOut.ToString());
                            writer.WriteAttributeString("IsOptional", parameter.IsOptional.ToString());
                            writer.WriteAttributeString("HasDefaultValue", parameter.HasDefaultValue.ToString());
                            if (parameter.HasDefaultValue) writer.WriteAttributeString("DefaultValue", parameter.DefaultValue.ToString());
                            writer.WriteEndElement(); // Parameter
                        }
                        writer.WriteEndElement(); // Parameters

                        writer.WriteEndElement(); // Constructor

                    }
                    writer.WriteEndElement(); // Constructors

                    writer.WriteStartElement("Methods");
                    foreach (MethodInfo method in type.GetMethods())
                    {
                        writer.WriteStartElement("Method");
                        writer.WriteAttributeString("ReturnType", method.ReturnType.Name);
                        writer.WriteAttributeString("Name", method.Name);
                        writer.WriteAttributeString("IsPublic", method.IsPublic.ToString());
                        writer.WriteAttributeString("IsPrivate", method.IsPrivate.ToString());
                        writer.WriteAttributeString("IsStatic", method.IsStatic.ToString());
                        writer.WriteAttributeString("IsAbstract", method.IsAbstract.ToString());
                        writer.WriteAttributeString("IsVirtual", method.IsVirtual.ToString());
                        writer.WriteAttributeString("IsFinal", method.IsFinal.ToString());
                        writer.WriteStartElement("Parameters");
                        foreach (ParameterInfo parameter in method.GetParameters())
                        {
                            writer.WriteStartElement("Parameter");
                            writer.WriteAttributeString("Type", parameter.ParameterType.Name);
                            writer.WriteAttributeString("Name", parameter.Name);
                            writer.WriteAttributeString("IsIn", parameter.IsIn.ToString());
                            writer.WriteAttributeString("IsOut", parameter.IsOut.ToString());
                            writer.WriteAttributeString("IsOptional", parameter.IsOptional.ToString());
                            writer.WriteAttributeString("HasDefaultValue", parameter.HasDefaultValue.ToString());
                            if (parameter.HasDefaultValue) writer.WriteAttributeString("DefaultValue", parameter.DefaultValue.ToString());
                            writer.WriteEndElement(); // Parameter
                        }
                        writer.WriteEndElement(); // Parameters

                        writer.WriteEndElement(); // Method

                    }
                    writer.WriteEndElement(); // Methods

                    writer.WriteEndElement(); // Type

                }
                writer.WriteEndElement(); // Assembly
            }
        }
    }
}
