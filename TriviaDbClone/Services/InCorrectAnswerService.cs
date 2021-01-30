using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaDbClone.Entities;
using TriviaDbClone.Providers;
using TriviaDbClone.Providers.ProxyManager;
using TriviaDbClone.Repository;
using TriviaDbClone.UnitofWork;

namespace TriviaDbClone.Services
{
    public class InCorrectAnswerService : ControllerBase, IInCorrectAnswerService
    {
        private readonly ILogger<InCorrectAnswerService> _logger;
        private readonly IGenericRepository<incorrect_answer> _genericRepository;
        private readonly ICacheProvider _cacheProvider;
        private readonly IProxyManager _proxyManager;
        private readonly IUnitOfWork _unitOfWork;
        public InCorrectAnswerService(ILogger<InCorrectAnswerService> logger,
             IUnitOfWork unitOfWork,
             IProxyManager proxyManager,
        ICacheProvider cacheProvider)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _genericRepository = _unitOfWork.GetRepository<incorrect_answer>();
            _cacheProvider = cacheProvider;
            _proxyManager = proxyManager;
        }
    

        public IActionResult AddRange(List<incorrect_answer> inCorrectList)
        {
            _genericRepository.AddRange(inCorrectList);
            var result = _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return Ok(inCorrectList);
            }
            else
            {
                return BadRequest("Bad Request...");

            }
     
        }
    }
}
