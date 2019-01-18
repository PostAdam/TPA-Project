using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ModelDTG;
using Model.Reflection;
using Model.Reflection.Enums;

namespace ReflectionUnitTest
{
    [TestClass]
    [DeploymentItem(@"Instrumentation\TPA.ApplicationArchitecture.dll", @"Instrumentation")]
    public class ReflectorUnitTest
    {
        [TestMethod]
        public void ReflectorConstructorTest()
        {
            new Reflector();
            FileInfo _fileInfo = new FileInfo(ReflectorTestClass.TestAssemblyName);
            Assert.IsTrue(_fileInfo.Exists);
            Assert.IsNotNull(ReflectorTestClass.Reflector);
            Assert.IsNotNull(ReflectorTestClass.Reflector.MyNamespace);
            Assert.AreEqual<int>(4, ReflectorTestClass.Reflector.Namespaces.Count);
        }

        [TestMethod]
        public void AssemblyNameTest()
        {
            FileInfo fileInfo = new FileInfo(ReflectorTestClass.TestAssemblyName);
            Reflector reflector = new Reflector();
            reflector.Reflect(fileInfo.Directory + ReflectorTestClass.TestAssemblyName).Wait();
            Assert.AreEqual(Path.GetFileName(ReflectorTestClass.TestAssemblyName), reflector.AssemblyModel.Name);
        }

        [TestMethod]
        public void CircularReferencesShouldNotCreateNewObjects()
        {
            TypeMetadata classA =
                ReflectorTestClass.Reflector.CircularNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "ClassA");
            TypeMetadata classB = classA.Properties.Single(x => x.Name == "ClassB").TypeMetadata;
            TypeMetadata classA2 = classB.Properties.Single(x => x.Name == "ClassA").TypeMetadata;
            TypeMetadata classB2 = classA2.Properties.Single(x => x.Name == "ClassB").TypeMetadata;
            TypeMetadata classA3 = classB2.Properties.Single(x => x.Name == "ClassA").TypeMetadata;

            Assert.AreEqual(classA2, classA3);

        }

        [TestMethod]
        public void AbstractClassTest()
        {
            TypeMetadata abstractClass =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "AbstractClass");
            Assert.AreEqual(AbstractEnum.Abstract, abstractClass.Modifiers.Item3);
            Assert.AreEqual(AbstractEnum.Abstract,
                abstractClass.Methods.Single(x => x.Name == "AbstractMethod").Modifiers.Item2);
        }

        [TestMethod]
        public void ClassWithAttributesTest()
        {
            TypeMetadata attributeClass =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "ClassWithAttribute");
            Assert.AreEqual(1, attributeClass.Attributes.Count());
        }

        [TestMethod]
        public void DerivedClassTest()
        {
            TypeMetadata derivedClass =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "DerivedClass");
            Assert.IsNotNull(derivedClass.BaseType);
        }

        [TestMethod]
        public void EnumTest()
        {
            TypeMetadata enums =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "Enum");
            Assert.AreEqual(TypeKind.Enum, enums.TypeKind);
        }

        [TestMethod]
        public void GenericClassTest()
        {
            TypeMetadata genericClass =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x =>
                    x.TypeName.Contains("GenericClass"));
            Assert.AreEqual(1, genericClass.GenericArguments.Count());
            Assert.AreEqual<string>("T", genericClass.GenericArguments.Single().TypeName);
            Assert.AreEqual<string>("T",
                genericClass.Properties.Single<PropertyMetadata>(x => x.Name == "GenericProperty").TypeMetadata
                    .TypeName);
            Assert.AreEqual<int>(1,
                genericClass.Methods.Single<MethodMetadata>(x => x.Name == "GenericMethod").Parameters.Count());
            Assert.AreEqual<string>("T",
                genericClass.Methods.Single<MethodMetadata>(x => x.Name == "GenericMethod").Parameters.Single()
                    .TypeMetadata.TypeName);
            Assert.AreEqual<string>("T",
                genericClass.Methods.Single<MethodMetadata>(x => x.Name == "GenericMethod").ReturnType
                    .TypeName);
            //TypeMetaData lacks Fields info
        }

        [TestMethod]
        public void InterfaceTest()
        {
            TypeMetadata interfaceClass =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "IExample");
            Assert.AreEqual(TypeKind.Interface, interfaceClass.TypeKind);
            Assert.AreEqual(AbstractEnum.Abstract, interfaceClass.Modifiers.Item3);
            Assert.AreEqual(AbstractEnum.Abstract,
                interfaceClass.Methods.Single<MethodMetadata>(x => x.Name == "MethodA").Modifiers.Item2);
        }

        [TestMethod]
        public void ImplementedInterfaceTest()
        {
            TypeMetadata _interfaceClass =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "IExample");
            TypeMetadata implementedInterfaceClass =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x =>
                    x.TypeName == "ImplementationOfIExample");
            Assert.AreEqual<string>("IExample", implementedInterfaceClass.ImplementedInterfaces.Single().TypeName);
            foreach (MethodMetadata method in _interfaceClass.Methods)
                Assert.IsNotNull(implementedInterfaceClass.Methods.SingleOrDefault(x => x.Name == method.Name));
        }

        [TestMethod]
        public void Linq2SQLClassTest()
        {
            TypeMetadata linq2Sql =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "Linq2SQL");
            Assert.AreEqual(TypeKind.Class, linq2Sql.TypeKind);
            Assert.AreEqual(AccessLevel.Private, linq2Sql.Modifiers.Item1);
            MethodMetadata connect = linq2Sql.Methods.Single(x => x.Name == "Connect");
            Assert.IsNotNull(connect);
        }

        [TestMethod]
        public void NestedClassTest()
        {
            TypeMetadata outerClass =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x =>
                    x.TypeName == "OuterClass");
            Assert.IsTrue(true);
            // TypeMetaData doesn't store information about private nested types (it does about public)
        }

        [TestMethod]
        public void StaticClassTest()
        {
            TypeMetadata staticClass =
                ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "StaticClass");
            Assert.AreEqual(StaticEnum.Static,
                staticClass.Methods.Single(x => x.Name == "StaticMethod1").Modifiers.Item3);
            // no information about class/property being static
            // TypeMetaData lacks Fields info
        }

        [TestMethod]
        public void StructureTest()
        {
            TypeMetadata structure = ReflectorTestClass.Reflector.AssemblyModel.Namespaces
                .Single(x => x.NamespaceName == "TPA.ApplicationArchitecture.Data")
                .Types.Single(x => x.TypeName == "Structure");
            Assert.AreEqual(TypeKind.Struct, structure.TypeKind);
        }

        private class ReflectorTestClass : Reflector
        {
            internal static ReflectorTestClass Reflector => m_Reflector.Value;
            internal Dictionary<string, NamespaceMetadata> Namespaces;
            internal NamespaceMetadata MyNamespace { get; private set; }
            internal NamespaceMetadata CircularNamespace { get; private set; }

            internal ReflectorTestClass()
            {
                FileInfo _fileInfo = new FileInfo(ReflectorTestClass.TestAssemblyName);
                this.Reflect(_fileInfo.Directory + TestAssemblyName).Wait();
                Namespaces =
                    this.AssemblyModel.Namespaces.ToDictionary<NamespaceMetadata, string>(x => x.NamespaceName);
                MyNamespace = Namespaces.ContainsKey(m_NamespaceName)
                    ? Namespaces["TPA.ApplicationArchitecture.Data"]
                    : null;
                CircularNamespace = Namespaces.ContainsKey(m_NamespaceName)
                    ? Namespaces[ "TPA.ApplicationArchitecture.Data.CircularReference" ]
                    : null;

            }

            internal const string TestAssemblyName =
                @"..\..\..\ReflectionUnitTest\Instrumentation\TPA.ApplicationArchitecture.dll";

            #region private

            private const string m_NamespaceName = "TPA.ApplicationArchitecture.Data";

            private static Lazy<ReflectorTestClass> m_Reflector =
                new Lazy<ReflectorTestClass>(() => new ReflectorTestClass());

            #endregion
        }
    }
}