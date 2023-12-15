import axios from "axios";

import * as models from "@/types/models";
import { type IFileClient } from "@/interfeces/IFileClient";

const API_URL: string = "http://localhost:5181/fileapi";

class FileClient implements IFileClient {
  async createFile(fileInfo: models.FileInfo): Promise<string> {
    return await axios.post(`${API_URL}`, fileInfo, {
      headers: {
        "Content-Type": "application/json"
      }
    }).then(response => response.data);
  }

  async uploadFile(id: string, file: File, onprogress: Function, cancelprogress: Function): Promise<any> {
    return await axios.put(`${API_URL}/upload/${id}`, file, {
      headers: {
        "Content-Type": "application/octet-stream"
      },
      cancelToken: new axios.CancelToken(c => { // Исполнительная функция (или executor function) получает функцию отмены в качестве параметра
        cancelprogress(c);
      }),
      onUploadProgress: progressEvent => {
        onprogress(Math.round(progressEvent.progress! * 100));
      }

    }).then(response => response.data)
      .catch(exeption => {
        if (axios.isCancel(exeption)) {
          throw exeption.message;
        }
      });
  }

  async downloadFile(): Promise<any> {
    await axios.put(`${API_URL}/download/`, {}, {
      headers: {
        "Content-Type": "application/octet-stream"
      }
    }).then(response => response.data) ;
  }
}

export default FileClient;
