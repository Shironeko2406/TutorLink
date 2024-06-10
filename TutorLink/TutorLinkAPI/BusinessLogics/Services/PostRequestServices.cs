using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using TutorLinkAPI.BusinessLogics.IServices;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class PostRequestServices : IPostRequestService
    {
        private readonly PostRequestRepository _postRequestRepository;

        public PostRequestServices(PostRequestRepository postRequestRepository)
        {
            _postRequestRepository = postRequestRepository;
        }

        public List<PostRequest> GetAllPostRequests() => _postRequestRepository.GetAll().ToList();

        public PostRequest GetPostRequestById(Guid postId) => _postRequestRepository.Get(pr => pr.PostId == postId);

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

        public void DeletePostRequest(Guid postId)
        {
            _postRequestRepository.Delete(postId);
            _postRequestRepository.SaveChanges();
        }
    }
}
