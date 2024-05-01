ry>이 정보를 설명하는 <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> 값입니다.</summary>
    </member>
    <member name="T:System.Runtime.InteropServices.ComTypes.TYPEDESC">
      <summary>변수의 형식, 함수의 반환 형식 또는 함수 매개 변수의 형식을 설명합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEDESC.lpValue">
      <summary>변수가 VT_SAFEARRAY나 VT_PTR이면, lpValue 필드에 요소 형식을 지정하는 TYPEDESC에 대한 포인터가 들어 있습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEDESC.vt">
      <summary>이 TYPEDESC가 설명한 항목에 대한 변형 형식을 나타냅니다.</summary>
    </member>
    <member name="T:System.Runtime.InteropServices.ComTypes.TYPEFLAGS">
      <summary>형식 설명의 속성과 특성을 정의합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FAGGREGATABLE">
      <summary>이 클래스는 집계를 지원합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FAPPOBJECT">
      <summary>Application 개체를 설명하는 형식 설명입니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FCANCREATE">
      <summary>이 형식의 인스턴스는 ITypeInfo::CreateInstance가 만들 수 있습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FCONTROL">
      <summary>이 형식은 다른 형식을 파생시킨 컨트롤이며, 사용자에게 표시되지 않아야 합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FDISPATCHABLE">
      <summary>이 인터페이스가 IDispatch에서 직접적이든 간접적이든 파생됨을 나타냅니다.이 플래그는 계산되므로 해당 플래그에 대한 ODL(Object Description Language)이 없습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FDUAL">
      <summary>이 인터페이스는 IDispatch와 VTBL 바인딩을 모두 지원합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FHIDDEN">
      <summary>이 형식이 브라우저에 나타나면 안 됩니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FLICENSED">
      <summary>이 형식은 허가되었습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FNONEXTENSIBLE">
      <summary>이 인터페이스는 런타임에서 멤버를 추가할 수 없습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FOLEAUTOMATION">
      <summary>인터페이스에서 사용된 형식은 VTBL바인딩 지원을 포함하여 Automation과 완벽하게 호환됩니다.인터페이스에서 dual을 설정하면 이 플래그와 <see cref="F:System.Runtime.InteropServices.TYPEFLAGS.TYPEFLAG_FDUAL" />이 모두 설정됩니다.dispinterface에서는 이 플래그를 사용할 수 없습니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FPREDECLID">
      <summary>이 형식은 미리 정의됩니다.클라이언트 응용 프로그램은 이 특성을 가진 개체의 단일 인스턴스를 자동으로 만들어야 합니다.개체를 가리키는 변수의 이름은 개체의 클래스 이름과 동일합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FPROXY">
      <summary>인터페이스가 프록시/스텁 동적 연결 라이브러리를 사용할 것임을 나타냅니다.이 플래그는 형식 라이브러리가 등록 취소될 때에도 형식 라이브러리 프록시의 등록이 취소되지 않음을 지정합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FREPLACEABLE">
      <summary>이 개체는 IConnectionPointWithDefault를 지원하며 기본 동작을 수행합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FRESTRICTED">
      <summary>매크로 언어에서 액세스해서는 안 됩니다.이 플래그는 시스템 수준 형식 또는 형식 브라우저가 표시하지 않는 형식을 위한 것입니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FREVERSEBIND">
      <summary>자식을 확인하기 전에 기본 인터페이스의 이름을 확인을 했는지를 나타냅니다. 이것은 기본 동작과 반대입니다.</summary>
    </member>
    <member name="T:System.Runtime.InteropServices.ComTypes.TYPEKIND">
      <summary>데이터와 함수의 여러 형식을 지정합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_ALIAS">
      <summary>다른 형식의 별칭인 형식입니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_COCLASS">
      <summary>구현되는 구성 요소 인터페이스 집합입니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_DISPATCH">
      <summary>IDispatch::Invoke를 통해 액세스할 수 있는 메서드와 속성 집합입니다.기본적으로 이중 인터페이스는 TKIND_DISPATCH를 반환합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_ENUM">
      <summary>열거자 집합입니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_INTERFACE">
      <summary>가상 함수가 있는 형식입니다. 이때 모든 가상 함수는 순수 가상 함수입니다