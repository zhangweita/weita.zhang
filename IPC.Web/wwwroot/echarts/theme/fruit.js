��을 만든 시간을 나타냅니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.STATSTG.grfLocksSupported">
      <summary>이 스트림 또는 바이트 배열이 지원하는 영역 잠금 형식을 나타냅니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.STATSTG.grfMode">
      <summary>개체가 열렸을때 명시된 액세스 모드를 나타냅니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.STATSTG.grfStateBits">
      <summary>IStorage::SetStateBits 메서드가 설정한 가장 최근 값인 저장소 개체의 현재 상태를 나타냅니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.STATSTG.mtime">
      <summary>이 저장소, 스트림 또는 바이트 배열에 대한 최종 수정 시간을 나타냅니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.STATSTG.pwcsName">
      <summary>이 구조체가 설명하는 개체의 이름이 들어 있으며 null로 끝나는 문자열에 대한 포인터를 나타냅니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.STATSTG.reserved">
      <summary>다음에 사용하도록 예약됩니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.STATSTG.type">
      <summary>STGTY 열거형의 값 중 하나인 저장소 개체의 형식을 나타냅니다.</summary>
    </member>
    <member name="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM">
      <summary>STGMEDIUM 구조체의 관리되는 정의를 제공합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease">
      <summary>받는 프로세스가 ReleaseStgMedium 함수를 호출할 때 보내는 프로세스에서 저장소를 해제하는 방법을 제어할 수 있도록 하는 인터페이스 인스턴스에 대한 포인터를 나타냅니다.<see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" />가 null이면 ReleaseStgMedium에서는 기본 프로시저를 사용하여 저장소를 해제하고, 그렇지 않으면 ReleaseStgMedium에서는 지정된 IUnknown 인터페이스를 사용합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.tymed">
      <summary>저장 미디어 형식을 지정합니다.마샬링 및 역마샬링 루틴에서는 이 값을 사용하여 공용 구조체 멤버가 사용되었는지를 확인합니다.이 값은 <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> 열거형의 요소 중 하나여야 합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.unionmember">
      <summary>받는 프로세스에서 전송 중인 데이터에 액세스하는 데 사용할 수 있는 핸들, 문자열 또는 인터페이스 포인터를 나타냅니다.</summary>
    </member>
    <member name="T:System.Runtime.InteropServices.ComTypes.SYSKIND">
      <summary>대상 운영 체제 플랫폼을 식별합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.SYSKIND.SYS_MAC">
      <summary>형식 라이브러리에 대한 대상 운영 체제는 Apple Macintosh입니다.기본적으로 모든 데이터 필드는 짝수 바이트 경계로 맞추어집니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.SYSKIND.SYS_WIN16">
      <summary>형식 라이브러리에 대한 대상 운영 체제는 16비트 Windows 시스템입니다.기본적으로 데이터 필드는 패킹됩니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.SYSKIND.SYS_WIN32">
      <summary>형식 라이브러리에 대한 대상 운영 체제는 32비트 Windows 시스템입니다.기본적으로 데이터 필드는 저절로 맞추어지는데 예를 들면 2바이트 정수는 짝수 바이트 경계로, 4바이트 정수는 네 단어 경계 등으로 맞추어집니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.SYSKIND.SYS_WIN64">
      <summary>형식 라이브러리에 대한 대상 운영 체제는 64비트 Windows 시스템입니다.</summary>
    </member>
    <member name="T:System.Runtime.InteropServices.ComTypes.TYMED">
      <summary>TYMED 구조체의 관리되는 정의를 제공합니다.</summary>
    </member>
    <member name="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ENHMF">
      <summary>저장 미디어가 확장 메타파일입니다.<see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> 멤버가 null이면 대상 프로세스에서 D