using System;
namespace FaceIDSample
{
    public interface IDependencyService
    {
         string GetName();
        bool FaceIdAvailable();
        bool Authenticted();
        bool CheckFaceIDAvailable();
        bool CheckFingerPrintAvailable();
    }
}
