using System;
using EkranPaylas.Data.Domain;
using EkranPaylas.Data.Repository;

namespace EkranPaylas.Service
{
    public class ScreenShotService : IScreenShotService
    {
        private readonly IRepository<ScreenShot> _shotRepository;
        private readonly IStringGenerator _stringGenerator;

        public ScreenShotService(IRepository<ScreenShot> shotRepository, IStringGenerator stringGenerator)
        {
            _shotRepository = shotRepository;
            _stringGenerator = stringGenerator;
        }

        public string CreateScreenShot(string[] shotLinks)
        {
            var model = new ScreenShot
                            {
                                Links = shotLinks,
                                UploaderIpAddress = "127.0.0.1",
                                Id = _stringGenerator.GenerateString(StringGenerateOptions.IncludeCharAndDigits, 5)
                            };

            _shotRepository.Add(model);

            return model.Id;
        }

        public string[] GetScreenShotSources(string shotId)
        {
            var model = _shotRepository.GetById(shotId);
            
            return model != null ? model.Links : null;
        }
    }
}
