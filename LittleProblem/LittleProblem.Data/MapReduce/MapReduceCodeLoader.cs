using System.IO;
using MongoDB.Bson;

namespace LittleProblem.Data.MapReduce
{
    public static class MapReduceCodeLoader
    {
        private const string CodeFileDirectory = "./MapReduce/";
        private static readonly MapReduceInMemoryCache Cache = new MapReduceInMemoryCache();

        /// <summary>
        /// Will load code in the file given in parameter.
        /// </summary>
        /// <param name="fileName">Code file for map or reduce.</param>
        /// <returns>Code</returns>
        public static BsonJavaScript Load(string fileName)
        {
            if (!Cache.IsCached(fileName))
            {
                Cache.Save(fileName, new BsonJavaScript(LoadFileContent(fileName)));
            }
            return Cache.Get(fileName);
        }

        /// <summary>
        /// Will load code in the file given in parameter.
        /// </summary>
        /// <param name="fileName">Code file for map or reduce.</param>
        /// <returns>Code</returns>
        private static string LoadFileContent(string fileName)
        {
            if (!File.Exists(CodeFileDirectory + fileName))
            {
                throw new FileNotFoundException("The code file for map/reduce does not exist. File : " + fileName);
            }
            var textFile = new StreamReader(CodeFileDirectory + fileName);
            string code = textFile.ReadToEnd();
            textFile.Close();

            return code;
        }

    }
}
