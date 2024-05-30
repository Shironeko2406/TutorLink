using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using TutorLinkAPI.BusinessLogics.IServices;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class PostRequestService : IPostRequestService
    {
        private readonly IGenericRepository<PostRequest> _postRequestRepository;

        public PostRequestService(IGenericRepository<PostRequest> postRequestRepository)
        {
            _postRequestRepository = postRequestRepository;
        }

        public List<PostRequest> GetAllPostRequests() => _postRequestRepository.GetAll().ToList();

        public PostRequest GetPostRequestById(Guid postrequestId) => _postRequestRepository.Get(pr => pr.PostId == postrequestId);

        public void CreatePostRequest(PostRequest postRequest)
        {
            _postRequestRepository.Add(postRequest);
            _postRequestRepository.SaveChanges();
        }

        public void UpdatePostRequest(PostRequest postRequest)
        {
            _postRequestRepository.Update(postRequest);
            _postRequestRepository.SaveChanges();
        }

        public void DeletePostRequest(Guid postrequestId)
        {
            _postRequestRepository.Delete(postrequestId);
            _postRequestRepository.SaveChanges();
        }
    }
}
