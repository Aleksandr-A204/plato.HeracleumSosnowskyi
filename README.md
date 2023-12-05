# ___Разработка прототипа модуля для определения по данным ДЗЗ мест произрастания борщевика Сосновского___

## _Установка_
На сервере необходимо наличие установленного программного обеспечения SAGA GIS.

## _Описание алгоритма расчета по шагам_
__1.__ Внешний сервис вызывает по HTTP метод `CreateFile` с параметром `fileInfo` для действия контроллера `HttpPost`. К параметру `fileInfo` относятся данные(идентификатор файла(id), имя файла, MIME-тип файла, последнее изменеие, идентификатор потока файла)  
  __1.1.__ Эти данные записываюся в БД `MongoDb` и при этом сгенерируется id  
  __1.3.__ Создается кэш с ключом "fileKey", в него записываются эти данные тоже, в нём осуществляются срок действия 30сек и ограничение размера кэша 16  
  __1.4.__ Метод валидации проверяет на правильность id, Если истина, то возвращаются статус 200 и id. Иначе возвращаются 404 и сообщение об ошибке  
  >"Ошибка запроса при выборе файла. Идентификатор выбранного файла некорректный."  

__2.__ Внешний сервис вызывает по HTTP метод `Upload` с параметром id для действия контроллера `HttpPut`. К параметру id относится идентификатор загрузки полученный при вызове `CreateFile`  
  __2.1.__ Если типом контента запроса не является MIME-тип файла `application/octet-stream`, то возвращаются статус 400 и сообщение об ошибке  
  >"Этот тип контента запроса не подерживается"  

  __2.2.__ Если id является некорректным, то возвращаются 400 и сообщение об ошибке  
  >"Ошибка запроса при загрузке файла. Полученный при вызове CreateFile идентификатор загрузки файла некорректный."  

  __2.3.__ Если не существует кэша с ключом "fileKey", то информация о выбранном файле придется читать из БД `MongoDb`  
  __2.4.__ Информация о файле проверяется пустой, тогда возвращаются статус 404 и сообщение об ошибке  
  >"Что-то пошло не так."  

  __2.5.__ Тело запроса сохраняется в БД `MongoDb`. Тело запроса может быть предназначен потоком файла  
  __2.6.__ При хранении потока файла сгенерируется идентификатор потока файла  
  __2.7.__ Возвращается статус 200   

__3.__ Внешний сервис вызывает по HTTP метод `StartProcess`. Этот метод, являющийся действием контроллера `HttpPut`  
  __3.1.__ Запускается процесс относительным путем указания имени командного файла `script.bat`  
  __3.2.__ Формируется данный файл, в котором указывается:  
    __3.2.1.__ Изменение полной пути к каталогу выполняемого скрипта  
	__3.2.2.__ Переход в предыдущий каталог  
	__3.2.3.__ Если не существует каталога `Storage`, он создается  
	__3.2.4.__ Переход в каталог `Storage`  


[Вверх](#разработка-прототипа-модуля-для-определения-по-данным-ДЗЗ-мест-произрастания-борщевика-сосновского)