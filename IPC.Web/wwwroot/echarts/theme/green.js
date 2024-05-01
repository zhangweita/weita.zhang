 허용되지 않습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FREPLACEABLE">
      <summary>인터페이스가 기본 동작을 사용하는 것으로 표시합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FREQUESTEDIT">
      <summary>설정 시 속성을 직접 변경하려고 하면 IPropertyNotifySink::OnRequestEdit이 호출됩니다.OnRequestEdit가 구현되면 변경 사항이 적용될지 결정됩니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FRESTRICTED">
      <summary>매크로 언어에서 변수에 액세스해서는 안 됩니다.이 플래그는 시스템 수준 변수 또는 형식 브라우저가 표시되지 않는 변수를 위한 플래그입니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FSOURCE">
      <summary>변수는 이벤트의 소스인 개체를 반환합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FUIDEFAULT">
      <summary>변수가 사용자 인터페이스에 기본적으로 표시됩니다.</summary>
    </member>
    <member name="T:System.Runtime.InteropServices.ComTypes.VARKIND">
      <summary>변수의 종류를 정의합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.VARKIND.VAR_CONST">
      <summary>VARDESC 구조체는 기호화된 상수를 설명합니다.이 상수와 연결된 메모리는 없습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.VARKIND.VAR_DISPATCH">
      <summary>IDispatch::Invoke를 통해서만 변수에 액세스할 수 있습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.VARKIND.VAR_PERINSTANCE">
      <summary>변수가 형식의 필드 또는 멤버입니다.이 변수는 형식의 각 인스턴스 내에서 고정 오프셋 위치에 있습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.VARKIND.VAR_STATIC">
      <summary>변수의 인스턴스가 하나만 있습니다.</summary>
    </member>
  </members>
</doc>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       ﻿<?xml version="1.0" encoding="utf-8"?>
<doc>
  <assembly>
    <name>System.Runtime.InteropServices</name>
  </assembly>
  <members>
    <member name="T:System.DataMisalignedException">
      <summary>Исключение, которое выбрасывается, когда единица данных считывается или записывается по адресу, не кратному размеру данных.Этот класс не наследуется.</summary>
      <filterpriority>2</filterpriority>
    </member>
    <member name="M:System.DataMisalignedException.#ctor">
      <summary>Инициализирует новый экземпляр класса <see cref="T:System.DataMisalignedException" />. </summary>
    </member>
    <member name="M:System.DataMisalignedException.#ctor(System.String)">
      <summary>Инициализирует новый экземпляр класса <see cref="T:System.DataMisalignedException" />, используя указанное сообщение об ошибке.</summary>
      <param name="message">Объект <see cref="T:System.String" />, описывающий ошибку.Содержимое параметра <paramref name="message" /> должно быть понятным пользователю.Вызывающий оператор этого конструктора необходим, чтобы убедиться, локализована ли данная строка для текущего языка и региональных параметров системы.</param>
    </member>
    <member name="M:System.DataMisalignedException.#ctor(System.String,System.Exception)">
      <summary>Инициализирует новый экземпляр класса <see cref="T:System.DataMisalignedException" />, используя указанные сообщение об ошибке и исходное исключение.</summary>
      <param name="message">Объект <see cref="T:System.String" />, описывающий ошибку.Содержимое параметра <paramref