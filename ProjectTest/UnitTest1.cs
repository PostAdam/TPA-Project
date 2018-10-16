using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Model.Reflection.Model;
using Project.ViewModel;

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
            public Class1 field;
        }

        [TestMethod]
        public void DummyItemTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata( typeof(Class1) );
            TypeMetadataViewModel typeMetadataViewModel = new TypeMetadataViewModel( typeMetadata );
            Assert.IsNull( typeMetadataViewModel.Child.ElementAt( 0 ) );
        }

        [TestMethod]
        public void IsExpandedTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata( typeof(Class1) );
            TypeMetadataViewModel typeMetadataViewModel = new TypeMetadataViewModel( typeMetadata );
            Assert.IsFalse( typeMetadataViewModel.IsExpanded );


            typeMetadataViewModel.IsExpanded = true;
            Assert.IsTrue( typeMetadataViewModel.IsExpanded );
        }

        [TestMethod]
        public void ChildTest()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.LoadDll( @"..\..\ClassLibrary1.dll" );
            viewModel.InitTreeView( viewModel.AssemblyMetadata );

            Assert.IsNotNull( viewModel.AssemblyMetadata );

            AssemblyMetadata expectedAssembly = viewModel.AssemblyMetadata;

            DataContractSerializer serializer = new DataContractSerializer( expectedAssembly.GetType() );
            using (FileStream stream = File.Create( @"..\Test.Xml" ))
            {
                serializer.WriteObject( stream, expectedAssembly );
            }

            AssemblyMetadata examinedAssembly = null;
            DataContractSerializer deserializer = new DataContractSerializer( typeof(AssemblyMetadata) );
            using (FileStream stream = File.OpenRead( @"..\Test.Xml" ))
            {
                examinedAssembly = (AssemblyMetadata) deserializer.ReadObject( stream );
                viewModel.AddClassesToDirectory( examinedAssembly );
                viewModel.InitTreeView( examinedAssembly );
            }

            Assert.AreEqual( expectedAssembly.Name, examinedAssembly.Name );
            for (int i = 0; i < expectedAssembly.Namespaces.Count(); i++)
            {
                Assert.AreEqual( expectedAssembly.Namespaces.ElementAt( i ).NamespaceName,
                    examinedAssembly.Namespaces.ElementAt( i ).NamespaceName );
                for (int j = 0; j < expectedAssembly.Namespaces.ElementAt( i ).Types.Count(); j++)
                {
                    Assert.AreEqual(expectedAssembly.Namespaces.ElementAt(i).Types.ElementAt( j ).TypeName, examinedAssembly.Namespaces.ElementAt(i).Types.ElementAt(j).TypeName);
                }
            }
        }

        [TestMethod]
        public void ChildLifetimeTest()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.LoadDll(@"..\..\ClassLibrary1.dll");
            viewModel.InitTreeView(viewModel.AssemblyMetadata);

            Assert.IsNotNull(viewModel.AssemblyMetadata);

            AssemblyMetadata expectedAssembly = viewModel.AssemblyMetadata;

            AssemblyMetadataViewModel assemblyMetadataViewModel = new AssemblyMetadataViewModel(expectedAssembly);
            assemblyMetadataViewModel.IsExpanded = true;
            assemblyMetadataViewModel.IsExpanded = false;

            Assert.AreEqual(assemblyMetadataViewModel.Child.First().Name, "ClassLibrary1");
        }
    }
}