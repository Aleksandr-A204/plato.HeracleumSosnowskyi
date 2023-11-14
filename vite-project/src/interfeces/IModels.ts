import * as models from "../types/models";

export interface IFileClient {
  createFile(file: models.FileInfo): Promise<string>;
  uploadFile(id: string, file: File): Promise<any>;
}
