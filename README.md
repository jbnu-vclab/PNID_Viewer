# PNID_Viewer

### 사용법

* Image, Xml 파일 불러오기
  - 열기 > Image/Xml > 파일 선택, Image과 Xml파일을 여러 개 불러와 ListView에서 클릭으로 선택

* Xml 파일 내보내기
  - ListView에서 원하는 파일 우클릭, '내보내기' 클릭

* Xml을 Data Grid에 보이기
  - ListView에서 원하는 파일의 체크 박스를 체크 -> 보임, 체크 해제 -> 안 보임

* Box 추가
  - 마우스 드래그(좌클릭) + CTRL 2번
  
* Box 삭제
  - Datagrid에서 원하는 박스 선택 후 Del key 누르기
 
* Box 수정
  - 마우스 우클릭 + CTRL: 박스 우측 하단 점 변경
  - 마우스 우클릭 + SHIFT: 박스 좌측 상단 점 변경
  - 마우스 우클릭 + 이동: 박스 회전
  
* 줌/패닝
  - 마우스 휠: 줌
  - 마우스 좌클릭: 패닝 가능

### 추가해야 할 것
* Datagrid에서 행 선택 후 Delete key 누르면 행이 지워지긴 하지만 xml에서는 지워지지 않음
* Datagrid에 띄워진 xml파일이 무슨 색의 Box로 표현되고 있는지 보여주는 기능 필요
* Box클릭시 또는 Datagrid의 값 클릭시 해당 값을 가진 Datagrid의 행으로, 또는 Box로 패닝하는 기능 필요
