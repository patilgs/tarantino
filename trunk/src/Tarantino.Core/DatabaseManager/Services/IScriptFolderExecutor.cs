using StructureMap;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(Keys.Default)]
	public interface IScriptFolderExecutor
	{
		void ExecuteScriptsInFolder(string scriptBaseDirectory, string scriptDirectory, ConnectionSettings settings, ITaskObserver taskObserver);
	}
}