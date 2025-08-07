using System;

namespace EntityCoreNetStandardLibrary
{
    public static class Generator
    {
        public static string GetDbModels()
        {
            return new DbModels().TransformText();
        }
    }
}
