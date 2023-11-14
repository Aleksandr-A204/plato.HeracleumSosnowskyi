import axios from "axios";

import * as IModels from "@/interfeces/IModels";
import * as models from "@/types/models";

const API_URL: string = "http://localhost:5181/fileapi";

class FileClient implements IModels.IFileClient {
  async createFile({ name, type }: models.FileInfo): Promise<string> {
    return await axios.post(`${API_URL}`, {}, {
      headers: {
        "Plato-Filename": encodeURI(name),
        "Plato-Filetype": type
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
}

export default FileClient;
