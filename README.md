# PNID_Viewer

### 사용법

* Image, Xml 파일 불러오기
  - 열기 > Image/Xml > 파일 선택, Image파일은 1개만, Xml파일은 여러개 불러올 수 있다.

* Xml 파일 내보내기
  - ListView에서 원하는 파일 우클릭, '내보내기' 클릭

* Box 추가
  - ctrl + 마우스 좌클릭으로 시작점 지정 후 드래그, 다시 ctrl + 마우스 좌클릭으로 추가 종료
  - 드래그 시 Ctrl를 누르지 않으면 좌클릭으로 패닝도 가능
  - ListView에서 보기를 누른, 즉 Datagrid에 띄워진 xml파일에 추가됨
  - 추가 후name을 입력하지 않으면 파일을 내보낸 후, Xml안에서 '_'로 표시됨
  
* Xml을 Data Grid에 보이기
  - ListView에서 원하는 파일 우클릭, '보기' 클릭
  
* 줌/패닝
  - 마우스 휠로 줌, 좌클릭으로 패닝 가능
  - 마우스 우클릭시 줌/패닝 리셋

### 추가해야 할 것
* Datagrid에서 행 선택 후 Delete key 누르면 행이 지워지긴 하지만 xml에서는 지워지지 않음
* Datagrid에 띄워진 xml파일이 무슨 색의 Box로 표현되고 있는지 보여주는 기능 필요
* Box클릭시 또는 Datagrid의 값 클릭시 해당 값을 가진 Datagrid의 행으로, 또는 Box로 패닝하는 기능 필요
