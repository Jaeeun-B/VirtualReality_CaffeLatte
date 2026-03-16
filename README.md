# 가상현실 HW04 — 퀘스트 실습용 씬 제작

## 프로젝트 정보

| 항목 | 내용 |
|------|------|
| Unity 버전 | **2022.3.62f3** (반드시 동일 버전 사용) |
| SDK | Meta XR All-in-One SDK (설치 완료 상태) |
| 빌드 타겟 | Android (Meta Quest) |

---

## 시작...

### 1. Clone

```bash
git clone https://github.com/Jaeeun-B/VirtualReality_CaffeLatte.git
```

### 2. Unity에서 열기

1. Unity Hub > **Add** 클릭
2. `VirtualReality_CaffeLatte/HW04` 폴더 선택
3. Unity 버전 **2022.3.62f3**으로 열기
4. Library 자동 생성 (첫 열기 시 5~10분 소요 — 정상)

### 3. Meta XR SDK 설정 (각자 PC에서 수행)

프로젝트 열린 후 아래를 확인/수행:

1. **Build Settings** > Android 플랫폼으로 Switch (이미 되어있을 수 있음)
2. **Player Settings** > Other Settings > Scripting Backend: **IL2CPP** / Target Architecture: **ARM64**
3. **XR Plug-in Management** > Android 탭 > **Oculus** 체크
4. **Project Validation** > Android 탭 > **Fix All**
5. **Meta XR** (Project Setup Tool) > Android > **Apply All**

---

## 폴더 구조

```
HW04/
└── Assets/
    ├── _Shared/              ← 공용 Material, Prefab
    │   ├── Materials/
    │   └── Prefabs/
    ├── Zone1_Scale/           ← Zone 1 담당자 전용
    │   └── Scenes/
    ├── Zone2_Height/          ← Zone 2 담당자 전용
    │   └── Scenes/
    ├── Zone3_Interaction/     ← Zone 3 담당자 전용
    │   └── Scenes/
    └── Zone4_Movement/        ← Zone 4 담당자 전용
        └── Scenes/
```

---

## 작업 규칙

1. **자기 Zone 폴더 안에서만** 파일 생성/수정
2. **ProjectSettings/ 폴더는 수정 금지**
3. Material, Prefab도 자기 Zone 폴더 안에 생성 (공용이면 `_Shared/`에)
4. OVRPlayerController는 씬에 드래그만 (prefab 원본 수정 X)
5. commit 전에 **씬 저장** (Cmd+S / Ctrl+S)

---

## 🔄 Git 워크플로우

### 작업 완료 후 Push

```bash
git add .
git commit -m "Zone번호: 작업 내용"
git pull origin main
git push origin main
```

### 커밋 메시지 예시

```
Zone1: 의자 3개 배치 완료
Zone2: 10m 플랫폼 + 바닥 참조물 배치
Zone3: OVR Grabber 설정 + Cube/Sphere Grabbable 추가
Zone4: 이동 참조 오브젝트 6개 배치
Shared: 공용 Material 추가
```

### Pull 시 충돌이 나면

각자 다른 Zone 폴더에서 작업하므로 **정상적이면 충돌 안 남**. 만약 난다면:

- `ProjectSettings/` 충돌 → `git checkout --theirs ProjectSettings/파일명`
- 자기 Zone이 아닌 파일 충돌 → `git checkout --theirs 파일경로`

---

## Zone별 구현 가이드

### 공통 작업 (모든 Zone)

1. 자기 Zone 폴더의 Scenes 안에서 새 씬 생성 (우클릭 > Create > Scene)
2. **Main Camera 삭제** (OVRPlayerController 안에 VR 카메라 포함)
3. **Plane** 추가 (바닥)
4. **OVRPlayerController** 추가 (Project 패널에서 `ovrplayer` 검색 → prefab 드래그)

---

### Zone 1: Scale Experience (크기 경험)

> VR에서 스케일 차이가 얼마나 극적으로 느껴지는지 체험

**씬 구성:**

```
Plane:              Position (0, 0, 0)     Scale (3, 1, 3)
OVRPlayerController: Position (0, 0, 0)

Chair_Small:         Position (-3, 0, 3)   Scale (0.5, 0.5, 0.5)
Chair_Normal:        Position (0, 0, 3)    Scale (1, 1, 1)
Chair_Giant:         Position (4, 0, 3)    Scale (5, 5, 5)
```

**의자 만들기 (Cube 조합):**

빈 게임오브젝트 `Chair` 만들고 아래를 자식으로:

```
Seat:  Position (0, 0.45, 0)       Scale (0.5, 0.05, 0.5)
Back:  Position (0, 0.75, -0.225)  Scale (0.5, 0.6, 0.05)
Leg1:  Position (0.2, 0.225, 0.2)    Scale (0.05, 0.45, 0.05)
Leg2:  Position (-0.2, 0.225, 0.2)   Scale (0.05, 0.45, 0.05)
Leg3:  Position (0.2, 0.225, -0.2)   Scale (0.05, 0.45, 0.05)
Leg4:  Position (-0.2, 0.225, -0.2)  Scale (0.05, 0.45, 0.05)
```

Chair를 Prefab으로 만든 후 3개 복제 → 각각 부모 Scale 변경 (0.5 / 1 / 5)

---

### Zone 2: Height Experience (높이 경험)

> 높이 10m 플랫폼 위에서 고소공포증 체험

**씬 구성:**

```
Plane (바닥):        Position (0, 0, 0)     Scale (5, 1, 5)
OVRPlayerController: Position (0, 10.1, 0)  ← 상판 위!

기둥 (Cube):         Position (0, 5, 0)     Scale (2, 10, 2)
상판 (Cube):         Position (0, 10, 0)    Scale (4, 0.2, 4)

[바닥 참조물 — 높이감 강화용]
Cube:               Position (3, 0.5, 3)    Scale (1, 1, 1)
Cube:               Position (-2, 0.5, 5)   Scale (1, 1, 1)
Sphere:             Position (0, 0.5, -3)   Scale (1, 1, 1)
```

> OVRPlayerController의 Y를 10.1로 설정 (상판에 끼지 않도록)

---

### Zone 3: Interaction Experience (손 인터랙션)

> 컨트롤러로 오브젝트를 잡고 던지는 체험

**컨트롤러 Grab 설정:**

OVRPlayerController를 펼쳐서:

`OVRCameraRig > TrackingSpace > LeftControllerAnchor`:
1. Add Component → **OVR Grabber**
2. Add Component → **Sphere Collider** (Is Trigger ✅, Radius: 0.1)
3. OVR Grabber의 Grab Volumes에 Sphere Collider 드래그

`RightControllerAnchor`에도 동일 반복

**잡을 오브젝트:**

```
Cube:   Position (0.5, 1, 2)   Scale (0.15, 0.15, 0.15)
        + Rigidbody (Mass: 0.1) + OVR Grabbable

Sphere: Position (-0.5, 1, 2)  Scale (0.15, 0.15, 0.15)
        + Rigidbody (Mass: 0.1) + OVR Grabbable
```

**트러블슈팅:**

| 문제 | 해결 |
|------|------|
| 오브젝트 못 잡음 | Grab Volumes에 Sphere Collider 연결 확인, Is Trigger 체크 확인 |
| 바닥 뚫고 떨어짐 | Plane에 Mesh Collider 있는지 확인 |

---

### Zone 4: Movement Experience (이동 방식 체험)

> 조이스틱으로 VR 공간을 돌아다니는 체험

**씬 구성:**

```
Plane:              Position (0, 0, 0)       Scale (5, 1, 5)
OVRPlayerController: Position (0, 0, 0)

[참조 오브젝트 — 이동감 확보용, 색상별 Material 적용]
Cube_A:     Position (5, 1, 5)       Scale (2, 2, 2)        빨강
Cube_B:     Position (-5, 1.5, 8)    Scale (3, 3, 3)        파랑
Cylinder_A: Position (0, 2, 15)      Scale (1, 4, 1)        초록
Sphere_A:   Position (8, 0.5, -3)    Scale (1, 1, 1)        노랑
Cube_C:     Position (-8, 0.75, 12)  Scale (1.5, 1.5, 1.5)  보라
Cylinder_B: Position (3, 1, -8)      Scale (0.5, 2, 0.5)    주황
```

**조작법:**
- 왼쪽 조이스틱: 전후좌우 이동
- 오른쪽 조이스틱: 좌우 회전

---


