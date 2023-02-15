using MediatR;


namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    //devuelve una lista de videos view model
    public class GetVideosListQuery : IRequest<List<VideosVm>>
    {
        public string _Username { get; set; } = String.Empty;

        public GetVideosListQuery(string username) 
        { 
            //si username es null que lance una exception
            _Username = username ?? throw new ArgumentNullException(nameof(username));
        }


    }
}
