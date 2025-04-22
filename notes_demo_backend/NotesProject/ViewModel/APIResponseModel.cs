namespace NotesProject.ViewModel
{
  public class APIResponseModel<T>
  {
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public int StatusCode { get; set; }

    // Constructor
    public APIResponseModel(bool success, string message, T data, int statusCode)
    {
      Success = success;
      Message = message;
      Data = data;
      StatusCode = statusCode;
    }

    // Default constructor for when no data is needed
    public APIResponseModel()
    {
      Success = true;
      Message = string.Empty;
      Data = default;
      StatusCode = 200; // Default to 200 OK
    }
  }


}
