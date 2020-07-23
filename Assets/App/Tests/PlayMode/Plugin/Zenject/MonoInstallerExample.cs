using UnityEngine;
using Zenject;


namespace Ling.Tests.PlayMode.Plugin.ZenjectTest
{
	public class MonoInstallerExample : MonoInstaller
	{
		/// <summary>
		/// �����Ńo�C���h����
		/// </summary>
		public override void InstallBindings()
		{
			Container.Bind<IExample>().To<Example>().AsSingle();

			// �ႤID
			Container.Bind<IExampleIdTest>().WithId(ExampleIdTest.ID.First).To<ExampleIdTest>().AsCached();
			Container.Bind<IExampleIdTest>().WithId(ExampleIdTest.ID.Second).To<ExampleIdTest>().AsCached();
		}
	}
}