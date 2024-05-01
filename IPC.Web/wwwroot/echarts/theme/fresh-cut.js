"T:System.Guid" />로, 참조로 전달됩니다. </param>
      <param name="ppTInfo">이 메서드는 반환될 때 요청한 ITypeInfo 인터페이스를 포함합니다.이 매개 변수는 초기화되지 않은 상태로 전달됩니다.</param>
    </member>
    <member name="M:System.Runtime.InteropServices.ComTypes.ITypeLib2.GetTypeInfoType(System.Int32,System.Runtime.InteropServices.ComTypes.TYPEKIND@)">
      <summary>형식 설명의 형식을 검색합니다.</summary>
      <param name="index">형식 라이브러리에 있는 형식 설명의 인덱스입니다. </param>
      <param name="pTKind">이 메서드는 반환될 때 형식 설명의 TYPEKIND 열거형에 대한 참조를 포함합니다.이 매개 변수는 초기화되지 않은 상태로 전달됩니다.</param>
    </member>
    <member name="M:System.Runtime.InteropServices.ComTypes.ITypeLib2.IsName(System.String,System.Int32)">
      <summary>라이브러리에 설명되어 있는 형식이나 멤버의 이름이 전달된 문자열에 들어 있는지 여부를 나타냅니다.</summary>
      <returns>
        <paramref name="szNameBuf" />가 형식 라이브러리에 있으면 true이고, 그렇지 않으면 false입니다.</returns>
      <param name="szNameBuf">테스트할 문자열입니다. </param>
      <param name="lHashVal">
        <paramref name="szNameBuf" />의 해시 값입니다. </param>
    </member>
    <member name="M:System.Runtime.InteropServices.ComTypes.ITypeLib2.ReleaseTLibAttr(System.IntPtr)">
      <summary>
        <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeLib.GetLibAttr(System.IntPtr@)" /> 메서드에서 처음 가져온 <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" /> 구조체를 해제합니다.</summary>
      <param name="pTLibAttr">해제할 TLIBATTR 구조체입니다. </param>
    </member>
    <member name="T:System.Runtime.InteropServices.ComTypes.LIBFLAGS">
      <summary>형식 라이브러리에 해당하는 플래그를 정의합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.LIBFLAGS.LIBFLAG_FCONTROL">
      <summary>형식 라이브러리는 컨트롤을 설명하고 보이지 않는 개체용 형식 브라우저에 표시되지 않아야 합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.LIBFLAGS.LIBFLAG_FHASDISKIMAGE">
      <summary>형식 라이브러리는 디스크에서 지속된 형식으로 존재합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.LIBFLAGS.LIBFLAG_FHIDDEN">
      <summary>형식 라이브러리는 용도가 제한되어 있지 않지만 사용자에게 표시되어서는 안 됩니다.형식 라이브러리는 컨트롤에 의해 사용되어야 합니다.호스트는 확장된 속성을 사용하여 컨트롤을 래핑하는 새 형식 라이브러리를 만들어야 합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.LIBFLAGS.LIBFLAG_FRESTRICTED">
      <summary>형식 라이브러리는 제한되어 있으며, 사용자에게 표시되어서는 안 됩니다.</summary>
    </member>
    <member name="T:System.Runtime.InteropServices.ComTypes.PARAMDESC">
      <summary>구조체 요소, 매개 변수, 프로세스 간 함수 반환 값을 전달하는 방법에 대한 필요한 정보가 들어 있습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.PARAMDESC.lpVarValue">
      <summary>프로세스 간에 전달되는 값에 대한 포인터를 나타냅니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.PARAMDESC.wParamFlags">
      <summary>구조체 요소, 매개 변수 또는 반환 값을 설명하는 비트 마스크 값을 나타냅니다.</summary>
    </member>
    <member name="T:System.Runtime.InteropServices.ComTypes.PARAMFLAG">
      <summary>구조체 요소, 매개 변수 또는 함수 반환 값을 한 프로세스에서 다른 프로세스로 전송하는 방법을 설명합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FHASCUSTDATA">
      <summary>매개 변수에 사용자 지정 데이터가 있습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FHASDEFAULT">
      <summary>매개 변수에 정의된 기본 동작이 있습니다.</summary>
    </mem