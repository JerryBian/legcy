using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using CommonMark;
using Laobian.Infrastuture.Interface.Service;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Const;
using Laobian.Infrastuture.Entity.Blog;

namespace Laobian.Service.Component
{
    public class BlogService : IBlogService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ILog4BlogVisitRepository _log4BlogVisitRepository;

        public BlogService(
            IBlogPostRepository blogPostRepository,
            ILog4BlogVisitRepository log4BlogVisitRepository)
        {
            _blogPostRepository = blogPostRepository;
            _log4BlogVisitRepository = log4BlogVisitRepository;
        }

        public async Task AddPostAsync(BlogPost post)
        {
            post.HtmlContentCh = CommonMarkConverter.Convert(post.MdContentCh);
            post.HtmlContentEn = CommonMarkConverter.Convert(post.MdContentEn);
            await _blogPostRepository.AddAsync(post);
        }

        public async Task<List<BlogPost>> GetAllPostsAsync(bool onlyPublished = true)
        {
            List<BlogPost> items;
            if (onlyPublished)
            {
                items = await _blogPostRepository.SelectAsync(_ => _.Publish);
            }
            else
            {
                items = await _blogPostRepository.SelectAsync(_ => true);
            }

            items.ForEach(async _ =>
            {
                _.Visits = await GetPostVisit(_.Id);
            });

            return items;
        }

        public async Task<BlogPost> GetPostAsync(int id)
        {
            var item = await _blogPostRepository.FindAsync(id);
            if(item == null)
            {
                throw new Exception($"Could not find post by requested id = {id}");
            }

            item.Visits = await GetPostVisit(id);
            return item;
        }

        public async Task UpdatePostAsync(BlogPost post)
        {
            post.HtmlContentCh = CommonMarkConverter.Convert(post.MdContentCh);
            post.HtmlContentEn = CommonMarkConverter.Convert(post.MdContentEn);
            await _blogPostRepository.UpdateAsync(post);
        }

        public async Task DeletePostAsync(int id)
        {
            await _blogPostRepository.DeleteAsync(id);
        }

        public async Task<BlogPost> GetPostByUrl(string url)
        {
            var item = await _blogPostRepository.SingleAsync(_ => string.Equals(url, _.Url, StringComparison.OrdinalIgnoreCase));
            if (item == null)
            {
                throw new Exception($"Could not find post by requested url = {url}");
            }

            item.Visits = await GetPostVisit(item.Id);
            return item;
        }

        public async Task<List<BlogPost>> GetPostsByTag(string tag)
        {
            var items = await _blogPostRepository.SelectAsync(_ => _.Tags.IndexOf(tag, StringComparison.OrdinalIgnoreCase) != -1);
            items.ForEach(async _ => _.Visits = await GetPostVisit(_.Id));
            return items;
        }

        private Task<long> GetPostVisit(int postId)
        {
            return  _log4BlogVisitRepository.CountAsync(_ => _.Component == BlogComponent.Post.ToString() && _.PageId == postId.ToString());
        }
    }
}
