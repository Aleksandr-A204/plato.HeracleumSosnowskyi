import axios from "axios";

//import * as IModels from "@/interfeces/IModels";
import * as models from "@/types/models";

const API_URL: string = "http://localhost:5181/fileapi";

class FileClient {
  async createFile(fileInfo: models.FileInfo): Promise<string> {
    return await axios.post(`${API_URL}`, fileInfo, {
      headers: {
        "Content-Type": "application/json"
      }
    }).then(response => response.data);
  }

  async uploadFile(id: string, file: File): Promise<any> {
    return await axios.put(`${API_URL}/upload/${id}`, file, {
      headers: {
        "Content-Type": "application/octet-stream"
      }
    }).then(response => response.data);
  }

  async downloadFile(): Promise<any> {
    await axios.put(`${API_URL}/download/`, {}, {
      headers: {
        "Content-Type": "application/octet-stream"
      }
    }).then(response => response.data);
  }
}

export default FileClient;
