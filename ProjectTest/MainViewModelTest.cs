using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Reflection.MetadataModels;
using ViewModel;
using ViewModel.MetadataViewModels;

namespace ProjectTest
{
    [TestClass]
    public class ItemViewModelTest
    {
        private class Class1
        {
        }

        private class Class2
        {
#pragma warning disable 649
            // ReSharper disable once InconsistentNaming
            // ReSharper disable once UnusedMember.Global
            public Class1 field;
#pragma warning restore 649
        }

        [TestMethod]
        public void DummyItemTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(Class1));
            TypeMetadataViewModel typeMetadataViewModel = new TypeMetadataViewModel(typeMetadata);
            Assert.IsNull(typeMetadataViewModel.Children.ElementAt(0));
        }

        [TestMethod]
        public void IsExpandedTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(Class1));
            TypeMetadataViewModel typeMetadataViewModel = new TypeMetadataViewModel(typeMetadata);
            Assert.IsFalse(typeMetadataViewModel.IsExpanded);

            typeMetadataViewModel.IsExpanded = true;
            Assert.IsTrue(typeMetadataViewModel.IsExpanded);
        }

        [TestMethod]
        public void ChildTest()
        {
            MainViewModel viewModel = new MainViewModel(null);
            viewModel.LoadDll(@"..\..\ClassLibrary1.dll");
            viewModel.InitTreeView(viewModel.AssemblyMetadata);

            Assert.IsNotNull(viewModel.AssemblyMetadata);

            AssemblyMetadata expectedAssembly = viewModel.AssemblyMetadata;

            DataContractSerializer serializer = new DataContractSerializer(expectedAssembly.GetType());
            using (FileStream stream = File.Create(@"..\Test.Xml"))
            {
                serializer.WriteObject(stream, expectedAssembly);
            }

            AssemblyMetadata examinedAssembly = null;
            DataContractSerializer deserializer = new DataContractSerializer(typeof(AssemblyMetadata));
            using (FileStream stream = File.OpenRead(@"..\Test.Xml"))
            {
                examinedAssembly = (AssemblyMetadata) deserializer.ReadObject(stream);
                viewModel.AddClassesToDirectory(examinedAssembly);
                viewModel.InitTreeView(examinedAssembly);
            }

            Assert.AreEqual(expectedAssembly.Name, examinedAssembly.Name);
            for (int i = 0; i < expectedAssembly.Namespaces.Count(); i++)
            {
                Assert.AreEqual(expectedAssembly.Namespaces.ElementAt(i).NamespaceName,
                    examinedAssembly.Namespaces.ElementAt(i).NamespaceName);
                for (int j = 0; j < expectedAssembly.Namespaces.ElementAt(i).Types.Count(); j++)
                {
                    Assert.AreEqual(expectedAssembly.Namespaces.ElementAt(i).Types.ElementAt(j).TypeName,
                        examinedAssembly.Namespaces.ElementAt(i).Types.ElementAt(j).TypeName);
                }
            }
        }

        [TestMethod]
        public void ChildLifetimeTest()
        {
            MainViewModel viewModel = new MainViewModel(null);
            viewModel.LoadDll(@"..\..\ClassLibrary1.dll");
            viewModel.InitTreeView(viewModel.AssemblyMetadata);

            Assert.IsNotNull(viewModel.AssemblyMetadata);

            AssemblyMetadata expectedAssembly = viewModel.AssemblyMetadata;

            AssemblyMetadataViewModel assemblyMetadataBaseViewModel =
                new AssemblyMetadataViewModel(expectedAssembly) {IsExpanded = true};
            assemblyMetadataBaseViewModel.IsExpanded = false;

            Assert.AreEqual(assemblyMetadataBaseViewModel.Children.First().FullName, "ClassLibrary1");
        }
    }
}