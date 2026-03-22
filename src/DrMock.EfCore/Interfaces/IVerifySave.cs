using Moq;

namespace DrMock.EfCore.Interfaces
{
    internal interface IVerifySave
    {
        void VerifyChangesSaved();

        void VerifyChangesSaved(Times times);

        void VerifyChangesSavedOnce();

        void VerifyChangesNeverSaved();

        void VerifyChangesSavedAsync();

        void VerifyChangesSavedAsync(Times times);

        void VerifyChangesSavedOnceAsync();

        void VerifyChangesNeverSavedAsync();
    }
}