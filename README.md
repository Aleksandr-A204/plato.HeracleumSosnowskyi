# Разработка прототипа модуля для определения по данным ДЗЗ мест произрастания борщевика Сосновского

## Установка
На сервере необходимо наличие установленного программного обеспечения SAGA GIS.

## Описание алгоритма расчета по шагам
1. Наш внешний сервис вызывает по HTTP метод `CreateFile` с параметром `fileInfo`

```
[HttpPost]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
public async Task<IActionResult> CreateFile([FromBody] FileInfoApi fileInfo)
{
	await _repository.CreateFileInfoAsync(fileInfo);

	_memoryCache.Set("file", fileInfo, new MemoryCacheEntryOptions { 
		AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
		Size = 16
	});

	return ValidationHelper.IsIdValid(fileInfo.Id) ? Ok(new { fileId = fileInfo.Id }) :
		NotFound("Ошибка запроса при выборе файла. Идентификатор выбранного файла некорректный.");
}
```

2. Данные записываются туда то
3. Формируется кмд файл, в котором указывается

[Вверх](#разработка-прототипа-модуля-для-определения-по-данным-ДЗЗ-мест-произрастания-борщевика-сосновского)