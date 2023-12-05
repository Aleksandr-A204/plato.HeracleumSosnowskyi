# ___Разработка прототипа модуля для определения по данным ДЗЗ мест произрастания борщевика Сосновского___

## _Установка_
На сервере необходимо наличие установленного программного обеспечения SAGA GIS.

## _Описание алгоритма расчета по шагам_
1. Наш внешний сервис вызывает по HTTP метод `CreateFile` с параметром `fileInfo`. Этот метод, являющийся действием контроллера HttpPost.
К параметру `fileInfo` относятся данные(идентификатор файла(id), имя файла, MIME-тип файла, последнее изменеие, идентификатор потока файла).
	1.1 Эти данные записываюся в БД MongoDb.
	1.2 При записи этих данных в БД сгенерируется id.
	1.3 
	1.4 Если метод валидации проверяет на правильность id, то возвращаются статус `200` и id. Иначе возвращаются `404` и сообщение об ошибке 
	"Ошибка запроса при выборе файла. Идентификатор выбранного файла некорректный.".

2. Наш внешний сервис вызывает по HTTP метод `Upload` с параметром id. Этот метод, являющийся действием контроллера HttpPut.
К параметру id относится идентификатор загрузки полученный при вызове `CreateFile`.
	2. Если типом контента запроса не является MIME-тип файла `application/octet-stream`, то возвращаются статус `400` и какой-то тип контента запроса не подерживается.
	2. Если метод валидации проверяет на неправильность id, то возвращаются возвращаются `400` и сообщение об ошибке
	"Ошибка запроса при загрузке файла. Полученный при вызове CreateFile идентификатор загрузки файла некорректный.".
	2. Если не существует кэша с ключом file, то информация о выбранном файле придется читать из БД MongoDb.
	2. Информация о файле проверяется пустой ли, тогда возвращаются статус `404` и сообщение об ошибке "Что-то пошло не так."
	2. Тело запроса сохранятся в БД MongoDb. Тело запроса может быть предназначен потоком файла.
	2. При хранении потока файла сгенерируется идентификатор потока файла.
	2. Возвращается статус `200`.

[Вверх](#разработка-прототипа-модуля-для-определения-по-данным-ДЗЗ-мест-произрастания-борщевика-сосновского)