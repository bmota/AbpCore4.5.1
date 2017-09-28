using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.People.Dto;
using Abp.Domain.Repositories;
using Abp.Collections.Extensions;
using Abp.Extensions;

namespace MyCompanyName.AbpZeroTemplate.People
{
    public class PersonAppService : AbpZeroTemplateAppServiceBase, IPersonAppService
    {
        private readonly IRepository<Person> _personRepository;

        public PersonAppService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }
        public ListResultDto<PersonListDto> GetPeople(GetPeopleInput input)
        {
            var people = _personRepository
           .GetAll()
           .WhereIf(
               !input.Filter.IsNullOrEmpty(),
               p => p.Name.Contains(input.Filter) ||
                    p.Surname.Contains(input.Filter) ||
                    p.EmailAddress.Contains(input.Filter)
           )
           .OrderBy(p => p.Name)
           .ThenBy(p => p.Surname)
           .ToList();

            return new ListResultDto<PersonListDto>(ObjectMapper.Map<List<PersonListDto>>(people));
        }
    }
}
