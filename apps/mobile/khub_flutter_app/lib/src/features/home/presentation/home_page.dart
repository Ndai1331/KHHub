import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../account/presentation/change_password_page.dart';
import '../../account/presentation/profile_picture_page.dart';
import '../../auth/presentation/auth_view_model.dart';
import '../../auth/presentation/login_page.dart';
import 'category_feed_page.dart';
import '../../news/presentation/news_detail_page.dart';
import '../../places/presentation/place_detail_page.dart';
import '../../settings/presentation/settings_page.dart';

class HomePage extends StatefulWidget {
  const HomePage({super.key});

  static const String routeName = '/home';

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  final PageController _bannerController = PageController(viewportFraction: 0.92);
  int _bannerIndex = 0;
  int _bottomTabIndex = 0;

  @override
  void dispose() {
    _bannerController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final vm = context.watch<AuthViewModel>();
    final userName = (vm.currentUser['name'] ?? '').toString();
    final isGuest = !vm.isAuthenticated;
    final categories = <_CategoryItem>[
      const _CategoryItem(
        key: 'news',
        title: 'Tin tức',
        icon: Icons.newspaper_rounded,
        color: Color(0xFF1E88E5),
      ),
      const _CategoryItem(
        key: 'places',
        title: 'Địa điểm',
        icon: Icons.location_on_rounded,
        color: Color(0xFF00ACC1),
      ),
      const _CategoryItem(
        key: 'jobs',
        title: 'Việc làm',
        icon: Icons.work_rounded,
        color: Color(0xFF5E92F3),
      ),
      const _CategoryItem(
        key: 'food',
        title: 'Ẩm thực',
        icon: Icons.restaurant_rounded,
        color: Color(0xFF42A5F5),
      ),
      const _CategoryItem(
        key: 'gold',
        title: 'Giá vàng',
        icon: Icons.currency_exchange_rounded,
        color: Color(0xFFFFB300),
      ),
      const _CategoryItem(
        key: 'gas',
        title: 'Giá xăng',
        icon: Icons.local_gas_station_rounded,
        color: Color(0xFF7E57C2),
      ),
    ];
    final categoryFeeds = <String, List<CategoryFeedItem>>{
      'news': <CategoryFeedItem>[
        const CategoryFeedItem(
          title: 'Khanh Hoa day manh du lich xanh',
          subtitle: 'Nhiều chương trình mới thu hút du khách.',
          thumbnailUrl:
              'https://images.unsplash.com/photo-1518509562904-e7ef99cdcc86?q=80&w=400&auto=format&fit=crop',
          coverUrl:
              'https://images.unsplash.com/photo-1528127269322-539801943592?q=80&w=1600&auto=format&fit=crop',
          content: 'Du lịch xanh đang trở thành định hướng trọng tâm tại Khánh Hòa trong giai đoạn mới.',
          rating: 4.6,
          favoriteCount: 235,
          comments: <String>['Thong tin rat hay', 'Mong co them du lieu chi tiet'],
        ),
        const CategoryFeedItem(
          title: 'Gia xang du kien giam nhe',
          subtitle: 'Thi truong dau tho co dau hieu ha nhiet.',
          thumbnailUrl:
              'https://images.unsplash.com/photo-1517673132405-a56a62b18caf?q=80&w=400&auto=format&fit=crop',
          coverUrl:
              'https://images.unsplash.com/photo-1446776811953-b23d57bd21aa?q=80&w=1600&auto=format&fit=crop',
          content: 'Trong kỳ điều hành tới, nhiều chuyên gia dự báo giá xăng có thể giảm nhẹ.',
          rating: 4.1,
          favoriteCount: 128,
          comments: <String>['Hy vong giam that', 'Can cap nhat theo gio'],
        ),
      ],
      'places': <CategoryFeedItem>[
        const CategoryFeedItem(
          title: 'Thap Tram Huong',
          subtitle: 'Diem check-in noi bat o trung tam Nha Trang',
          thumbnailUrl:
              'https://images.unsplash.com/photo-1469474968028-56623f02e42e?q=80&w=400&auto=format&fit=crop',
          coverUrl:
              'https://images.unsplash.com/photo-1501785888041-af3ef285b470?q=80&w=1600&auto=format&fit=crop',
          content: 'Tháp Trầm Hương nằm gần bãi biển, phù hợp tham quan sáng và tối.',
          rating: 4.8,
          favoriteCount: 422,
          comments: <String>['Dep va sach', 'Khong gian dep cho gia dinh'],
          galleryUrls: <String>[
            'https://images.unsplash.com/photo-1521292270410-a8c4d716d518?q=80&w=800&auto=format&fit=crop',
            'https://images.unsplash.com/photo-1473116763249-2faaef81ccda?q=80&w=800&auto=format&fit=crop',
          ],
        ),
        const CategoryFeedItem(
          title: 'Bai bien Tran Phu',
          subtitle: 'Bo bien dep va nhon nhip bac nhat',
          thumbnailUrl:
              'https://images.unsplash.com/photo-1519046904884-53103b34b206?q=80&w=400&auto=format&fit=crop',
          coverUrl:
              'https://images.unsplash.com/photo-1507525428034-b723cf961d3e?q=80&w=1600&auto=format&fit=crop',
          content: 'Bãi biển Trần Phú nổi tiếng với cát mịn, nước trong và nhiều hoạt động giải trí.',
          rating: 4.7,
          favoriteCount: 389,
          comments: <String>['Rat dang de trai nghiem', 'Buoi chieu dep'],
          galleryUrls: <String>[
            'https://images.unsplash.com/photo-1506953823976-52e1fdc0149a?q=80&w=800&auto=format&fit=crop',
            'https://images.unsplash.com/photo-1493558103817-58b2924bce98?q=80&w=800&auto=format&fit=crop',
          ],
        ),
      ],
      'jobs': <CategoryFeedItem>[
        const CategoryFeedItem(
          title: 'Nhan vien sale du lich',
          subtitle: 'Muc luong 10-15 trieu tai Nha Trang',
          thumbnailUrl:
              'https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?q=80&w=400&auto=format&fit=crop',
          coverUrl:
              'https://images.unsplash.com/photo-1521791136064-7986c2920216?q=80&w=1600&auto=format&fit=crop',
          content: 'Công ty du lịch tuyển nhân viên sales có kinh nghiệm từ 1 năm.',
          rating: 4.3,
          favoriteCount: 95,
          comments: <String>['Da nop CV', 'Thong tin ro rang'],
        ),
      ],
      'food': <CategoryFeedItem>[
        const CategoryFeedItem(
          title: 'Top quan hai san ngon',
          subtitle: 'Danh sach quan an duoc yeu thich',
          thumbnailUrl:
              'https://images.unsplash.com/photo-1559339352-11d035aa65de?q=80&w=400&auto=format&fit=crop',
          coverUrl:
              'https://images.unsplash.com/photo-1551218808-94e220e084d2?q=80&w=1600&auto=format&fit=crop',
          content: 'Tổng hợp quán hải sản ngon, giá hợp lý và gần biển.',
          rating: 4.5,
          favoriteCount: 176,
          comments: <String>['Do an ngon', 'Nen dat ban truoc'],
        ),
      ],
      'gold': <CategoryFeedItem>[
        const CategoryFeedItem(
          title: 'Bang gia vang hom nay',
          subtitle: 'Cap nhat SJC va nhan 24K',
          thumbnailUrl:
              'https://images.unsplash.com/photo-1610375461246-83df859d849d?q=80&w=400&auto=format&fit=crop',
          coverUrl:
              'https://images.unsplash.com/photo-1621416894569-0f39ed31d247?q=80&w=1600&auto=format&fit=crop',
          content: 'Giá vàng trong nước biến động theo phiên, cập nhật liên tục theo giờ.',
          rating: 4.2,
          favoriteCount: 140,
          comments: <String>['Theo doi de dau tu', 'Can them so sanh'],
        ),
      ],
      'gas': <CategoryFeedItem>[
        const CategoryFeedItem(
          title: 'Gia xang va dau cap nhat',
          subtitle: 'Thong tin ky dieu hanh moi nhat',
          thumbnailUrl:
              'https://images.unsplash.com/photo-1615906655593-ad0386982a0f?q=80&w=400&auto=format&fit=crop',
          coverUrl:
              'https://images.unsplash.com/photo-1605899435973-ca2d1a56f21c?q=80&w=1600&auto=format&fit=crop',
          content: 'Bản tin giá xăng dầu tổng hợp theo thông báo mới nhất từ cơ quan điều hành.',
          rating: 4.0,
          favoriteCount: 88,
          comments: <String>['Can biet som de sap xep', 'Thong tin huu ich'],
        ),
      ],
    };
    final news = <_NewsItem>[
      const _NewsItem(
        title: 'Nha Trang du lich he 2026',
        subtitle: 'Du bao luong khach tang manh dip le.',
        thumbnailUrl:
            'https://images.unsplash.com/photo-1528127269322-539801943592?q=80&w=400&auto=format&fit=crop',
        coverUrl:
            'https://images.unsplash.com/photo-1507525428034-b723cf961d3e?q=80&w=1600&auto=format&fit=crop',
        content:
            'Nha Trang buoc vao mua du lich voi nhieu su kien van hoa, am thuc va giai tri. Du bao luong khach tang cao trong quy toi.',
        rating: 4.6,
        favoriteCount: 188,
        comments: <String>[
          'Bai viet rat hay, thong tin huu ich!',
          'Minh sap di Nha Trang, cam on thong tin.',
        ],
      ),
      const _NewsItem(
        title: 'Gia vang hom nay',
        subtitle: 'Cap nhat bien dong thi truong vang trong ngay.',
        thumbnailUrl:
            'https://images.unsplash.com/photo-1610375461246-83df859d849d?q=80&w=400&auto=format&fit=crop',
        coverUrl:
            'https://images.unsplash.com/photo-1621416894569-0f39ed31d247?q=80&w=1600&auto=format&fit=crop',
        content:
            'Gia vang trong nuoc tiep tuc dieu chinh theo xu huong thi truong quoc te. Nha dau tu can theo doi cac moc gia quan trong.',
        rating: 4.2,
        favoriteCount: 124,
        comments: <String>[
          'Tin gia vang cap nhat nhanh!',
          'Can them bieu do theo gio.',
        ],
      ),
      const _NewsItem(
        title: 'Tuyen dung viec lam moi',
        subtitle: 'Nhieu vi tri dang mo tai Khanh Hoa.',
        thumbnailUrl:
            'https://images.unsplash.com/photo-1521791136064-7986c2920216?q=80&w=400&auto=format&fit=crop',
        coverUrl:
            'https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?q=80&w=1600&auto=format&fit=crop',
        content:
            'Nhiều doanh nghiep dich vu, du lich va cong nghe dang mo rong tuyen dung. Co hoi viec lam da dang cho nguoi lao dong tre.',
        rating: 4.5,
        favoriteCount: 210,
        comments: <String>[
          'Da ung tuyen qua app, rat tien!',
          'Mong co them bo loc muc luong.',
        ],
      ),
    ];
    final places = <_PlaceItem>[
      const _PlaceItem(
        title: 'Thap Tram Huong',
        subtitle: 'Bieu tuong giua long thanh pho bien',
        thumbnailUrl:
            'https://images.unsplash.com/photo-1469474968028-56623f02e42e?q=80&w=400&auto=format&fit=crop',
        coverUrl:
            'https://images.unsplash.com/photo-1501785888041-af3ef285b470?q=80&w=1600&auto=format&fit=crop',
        description:
            'Thap Tram Huong la diem check-in noi bat tai Nha Trang, gan bien va khu pho di bo. Khu vuc xung quanh co nhieu hoat dong giai tri vao buoi toi.',
        galleryUrls: <String>[
          'https://images.unsplash.com/photo-1521292270410-a8c4d716d518?q=80&w=800&auto=format&fit=crop',
          'https://images.unsplash.com/photo-1473116763249-2faaef81ccda?q=80&w=800&auto=format&fit=crop',
          'https://images.unsplash.com/photo-1444703686981-a3abbc4d4fe3?q=80&w=800&auto=format&fit=crop',
        ],
        rating: 4.8,
        favoriteCount: 432,
        comments: <String>['Khung canh dep, rat dang de tham quan.', 'Buoi toi len den rat dep.'],
      ),
      const _PlaceItem(
        title: 'Bai bien Tran Phu',
        subtitle: 'Bai bien dep va nhon nhip nhat Nha Trang',
        thumbnailUrl:
            'https://images.unsplash.com/photo-1519046904884-53103b34b206?q=80&w=400&auto=format&fit=crop',
        coverUrl:
            'https://images.unsplash.com/photo-1469474968028-56623f02e42e?q=80&w=1600&auto=format&fit=crop',
        description:
            'Bai bien Tran Phu phu hop tam bien, chay bo, va thuong thuc am thuc dem. Nhieu khach san va quan cafe dep nam dọc bo bien.',
        galleryUrls: <String>[
          'https://images.unsplash.com/photo-1507525428034-b723cf961d3e?q=80&w=800&auto=format&fit=crop',
          'https://images.unsplash.com/photo-1506953823976-52e1fdc0149a?q=80&w=800&auto=format&fit=crop',
          'https://images.unsplash.com/photo-1493558103817-58b2924bce98?q=80&w=800&auto=format&fit=crop',
        ],
        rating: 4.7,
        favoriteCount: 389,
        comments: <String>['Bien xanh dep, rat thoang.', 'Gan trung tam nen di lai tien.'],
      ),
    ];
    final bannerItems = <_BannerItem>[
      _BannerItem(
        title: 'Khanh Hoa - Nha Trang',
        subtitle: 'Tin moi ve du lich, su kien, va uu dai.',
        image: 'assets/images/home_mock_reference.png',
      ),
      _BannerItem(
        title: 'Nhip song bien moi ngay',
        subtitle: 'Cap nhat nhanh gia vang, gia xang va thong tin can biet.',
        image: 'assets/images/home_mock_reference.png',
      ),
      _BannerItem(
        title: 'Co hoi viec lam moi',
        subtitle: 'Tong hop viec lam chat luong tai khu vuc Khanh Hoa.',
        image: 'assets/images/home_mock_reference.png',
      ),
    ];

    return Scaffold(
      appBar: AppBar(title: Text(vm.t('::Menu:Home'))),
      drawer: Drawer(
        child: ListView(
          children: <Widget>[
            const DrawerHeader(child: Text('KHHub')),
            ListTile(
              leading: const Icon(Icons.settings),
              title: Text(vm.t('AbpSettingManagement::Settings')),
              onTap: () => Navigator.pushNamed(context, SettingsPage.routeName),
            ),
            ListTile(
              leading: const Icon(Icons.password),
              title: Text(vm.t('AbpUi::ChangePassword')),
              onTap: isGuest ? null : () => Navigator.pushNamed(context, ChangePasswordPage.routeName),
            ),
            ListTile(
              leading: const Icon(Icons.person),
              title: Text(vm.t('AbpUi::ProfilePicture')),
              onTap: isGuest ? null : () => Navigator.pushNamed(context, ProfilePicturePage.routeName),
            ),
            ListTile(
              leading: Icon(isGuest ? Icons.login_rounded : Icons.logout),
              title: Text(vm.t(isGuest ? 'AbpUi::Login' : 'AbpUi::Logout')),
              onTap: () async {
                if (!context.mounted) return;
                if (isGuest) {
                  Navigator.pushNamed(context, LoginPage.routeName);
                  return;
                }
                await context.read<AuthViewModel>().logout();
                if (!context.mounted) return;
                Navigator.pushNamedAndRemoveUntil(context, LoginPage.routeName, (_) => false);
              },
            ),
          ],
        ),
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Text(
              userName.isEmpty ? '${vm.t('::Welcome')}, Khach!' : '${vm.t('::Welcome')}, $userName!',
              style: const TextStyle(
                color: Color(0xFF0D47A1),
                fontSize: 24,
                fontWeight: FontWeight.w800,
              ),
            ),
            const SizedBox(height: 4),
            Text(
              vm.t('::LongWelcomeMessage'),
              style: const TextStyle(color: Color(0xFF5C6B7A), fontSize: 14),
            ),
            const SizedBox(height: 14),
            SizedBox(
              height: 176,
              child: PageView.builder(
                controller: _bannerController,
                itemCount: bannerItems.length,
                onPageChanged: (int value) => setState(() => _bannerIndex = value),
                itemBuilder: (BuildContext context, int index) {
                  final item = bannerItems[index];
                  return Container(
                    margin: const EdgeInsets.only(right: 10),
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(18),
                      boxShadow: <BoxShadow>[
                        BoxShadow(
                          color: Colors.black.withValues(alpha: 0.1),
                          blurRadius: 16,
                          offset: const Offset(0, 6),
                        ),
                      ],
                    ),
                    child: ClipRRect(
                      borderRadius: BorderRadius.circular(18),
                      child: Stack(
                        fit: StackFit.expand,
                        children: <Widget>[
                          Image.asset(item.image, fit: BoxFit.cover),
                          Container(
                            decoration: BoxDecoration(
                              gradient: LinearGradient(
                                begin: Alignment.bottomCenter,
                                end: Alignment.topCenter,
                                colors: <Color>[
                                  Colors.black.withValues(alpha: 0.54),
                                  Colors.transparent,
                                ],
                              ),
                            ),
                          ),
                          Positioned(
                            left: 14,
                            right: 14,
                            bottom: 14,
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: <Widget>[
                                Text(
                                  item.title,
                                  style: const TextStyle(
                                    color: Colors.white,
                                    fontWeight: FontWeight.w700,
                                    fontSize: 16,
                                  ),
                                ),
                                const SizedBox(height: 4),
                                Text(
                                  item.subtitle,
                                  style: const TextStyle(
                                    color: Colors.white,
                                    fontWeight: FontWeight.w500,
                                    fontSize: 12,
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ],
                      ),
                    ),
                  );
                },
              ),
            ),
            const SizedBox(height: 10),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: List<Widget>.generate(
                bannerItems.length,
                (int index) => Container(
                  width: _bannerIndex == index ? 18 : 8,
                  height: 8,
                  margin: const EdgeInsets.symmetric(horizontal: 3),
                  decoration: BoxDecoration(
                    color: _bannerIndex == index ? const Color(0xFF1E88E5) : const Color(0xFFBFD9F5),
                    borderRadius: BorderRadius.circular(20),
                  ),
                ),
              ),
            ),
            const SizedBox(height: 12),
            _SectionHeader(
              title: 'Categories',
              actionLabel: 'View all',
              onTap: () {},
            ),
            const SizedBox(height: 10),
            GridView.builder(
              physics: const NeverScrollableScrollPhysics(),
              shrinkWrap: true,
              itemCount: categories.length,
              gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
                crossAxisCount: 3,
                mainAxisSpacing: 10,
                crossAxisSpacing: 10,
                childAspectRatio: 1.15,
              ),
              itemBuilder: (BuildContext context, int index) {
                final item = categories[index];
                return GestureDetector(
                  onTap: () {
                    final feeds = categoryFeeds[item.key] ?? <CategoryFeedItem>[];
                    Navigator.pushNamed(
                      context,
                      CategoryFeedPage.routeName,
                      arguments: CategoryFeedArgs(
                        categoryTitle: item.title,
                        items: feeds,
                      ),
                    );
                  },
                  child: Container(
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(14),
                    boxShadow: <BoxShadow>[
                      BoxShadow(
                        color: Colors.black.withValues(alpha: 0.05),
                        blurRadius: 10,
                        offset: const Offset(0, 4),
                      ),
                    ],
                  ),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: <Widget>[
                      Container(
                        width: 44,
                        height: 44,
                        decoration: BoxDecoration(
                          color: item.color.withValues(alpha: 0.14),
                          borderRadius: BorderRadius.circular(22),
                        ),
                        child: Icon(item.icon, color: item.color, size: 24),
                      ),
                      const SizedBox(height: 8),
                      Text(
                        item.title,
                        style: const TextStyle(fontWeight: FontWeight.w600, fontSize: 13),
                      ),
                    ],
                  ),
                ),
                );
              },
            ),
            const SizedBox(height: 22),
            _SectionHeader(title: 'News', actionLabel: 'See more', onTap: () {}),
            const SizedBox(height: 10),
            ...news.map(
              (_NewsItem item) => GestureDetector(
                onTap: () => Navigator.pushNamed(
                  context,
                  NewsDetailPage.routeName,
                  arguments: NewsDetailArgs(
                    title: item.title,
                    subtitle: item.subtitle,
                    coverUrl: item.coverUrl,
                    thumbnailUrl: item.thumbnailUrl,
                    content: item.content,
                    initialRating: item.rating,
                    initialFavoriteCount: item.favoriteCount,
                    initialComments: item.comments,
                  ),
                ),
                child: Container(
                margin: const EdgeInsets.only(bottom: 10),
                padding: const EdgeInsets.all(14),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(14),
                  boxShadow: <BoxShadow>[
                    BoxShadow(
                      color: Colors.black.withValues(alpha: 0.05),
                      blurRadius: 10,
                      offset: const Offset(0, 4),
                    ),
                  ],
                ),
                child: Row(
                  children: <Widget>[
                    ClipRRect(
                      borderRadius: BorderRadius.circular(12),
                      child: Image.network(
                        item.thumbnailUrl,
                        width: 56,
                        height: 56,
                        fit: BoxFit.cover,
                      ),
                    ),
                    const SizedBox(width: 12),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: <Widget>[
                          Text(
                            item.title,
                            style: const TextStyle(fontWeight: FontWeight.w700, fontSize: 14),
                          ),
                          const SizedBox(height: 4),
                          Text(
                            item.subtitle,
                            style: TextStyle(
                              fontSize: 12,
                              color: Colors.black.withValues(alpha: 0.62),
                            ),
                          ),
                        ],
                      ),
                    ),
                    const Icon(Icons.arrow_forward_ios_rounded, size: 14, color: Color(0xFF1E88E5)),
                  ],
                ),
              ),
              ),
            ),
            const SizedBox(height: 16),
            _SectionHeader(title: 'Địa điểm', actionLabel: 'See more', onTap: () {}),
            const SizedBox(height: 10),
            ...places.map(
              (_PlaceItem item) => GestureDetector(
                onTap: () => Navigator.pushNamed(
                  context,
                  PlaceDetailPage.routeName,
                  arguments: PlaceDetailArgs(
                    title: item.title,
                    subtitle: item.subtitle,
                    coverUrl: item.coverUrl,
                    thumbnailUrl: item.thumbnailUrl,
                    description: item.description,
                    galleryUrls: item.galleryUrls,
                    initialRating: item.rating,
                    initialFavoriteCount: item.favoriteCount,
                    initialComments: item.comments,
                  ),
                ),
                child: Container(
                  margin: const EdgeInsets.only(bottom: 10),
                  padding: const EdgeInsets.all(14),
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(14),
                    boxShadow: <BoxShadow>[
                      BoxShadow(
                        color: Colors.black.withValues(alpha: 0.05),
                        blurRadius: 10,
                        offset: const Offset(0, 4),
                      ),
                    ],
                  ),
                  child: Row(
                    children: <Widget>[
                      ClipRRect(
                        borderRadius: BorderRadius.circular(12),
                        child: Image.network(
                          item.thumbnailUrl,
                          width: 56,
                          height: 56,
                          fit: BoxFit.cover,
                        ),
                      ),
                      const SizedBox(width: 12),
                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: <Widget>[
                            Text(
                              item.title,
                              style: const TextStyle(fontWeight: FontWeight.w700, fontSize: 14),
                            ),
                            const SizedBox(height: 4),
                            Text(
                              item.subtitle,
                              style: TextStyle(
                                fontSize: 12,
                                color: Colors.black.withValues(alpha: 0.62),
                              ),
                            ),
                          ],
                        ),
                      ),
                      const Icon(Icons.arrow_forward_ios_rounded, size: 14, color: Color(0xFF1E88E5)),
                    ],
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
      bottomNavigationBar: Padding(
        padding: const EdgeInsets.fromLTRB(16, 0, 16, 16),
        child: Container(
          height: 66,
          decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.circular(33),
            boxShadow: <BoxShadow>[
              BoxShadow(
                color: Colors.black.withValues(alpha: 0.08),
                blurRadius: 16,
                offset: const Offset(0, 6),
              ),
            ],
          ),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: <_BottomTabItem>[
              _BottomTabItem(icon: Icons.home_rounded, label: 'Home'),
              _BottomTabItem(icon: Icons.grid_view_rounded, label: 'Explore'),
              _BottomTabItem(icon: Icons.notifications_rounded, label: 'Alerts'),
              _BottomTabItem(icon: Icons.person_rounded, label: 'Profile'),
            ].asMap().entries.map((entry) {
              final index = entry.key;
              final item = entry.value;
              final selected = index == _bottomTabIndex;
              return GestureDetector(
                onTap: () => setState(() => _bottomTabIndex = index),
                child: AnimatedContainer(
                  duration: const Duration(milliseconds: 220),
                  padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
                  decoration: BoxDecoration(
                    color: selected ? const Color(0xFF1E88E5).withValues(alpha: 0.12) : Colors.transparent,
                    borderRadius: BorderRadius.circular(24),
                  ),
                  child: Row(
                    children: <Widget>[
                      Icon(
                        item.icon,
                        size: 22,
                        color: selected ? const Color(0xFF1E88E5) : const Color(0xFF8A9AB0),
                      ),
                      if (selected) ...<Widget>[
                        const SizedBox(width: 6),
                        Text(
                          item.label,
                          style: const TextStyle(
                            color: Color(0xFF1E88E5),
                            fontWeight: FontWeight.w700,
                          ),
                        ),
                      ],
                    ],
                  ),
                ),
              );
            }).toList(),
          ),
        ),
      ),
    );
  }
}

class _SectionHeader extends StatelessWidget {
  const _SectionHeader({
    required this.title,
    required this.actionLabel,
    required this.onTap,
  });

  final String title;
  final String actionLabel;
  final VoidCallback onTap;

  @override
  Widget build(BuildContext context) {
    return Row(
      children: <Widget>[
        Text(
          title,
          style: const TextStyle(fontSize: 18, fontWeight: FontWeight.w700),
        ),
        const Spacer(),
        TextButton(
          onPressed: onTap,
          child: Text(
            actionLabel,
            style: const TextStyle(
              color: Color(0xFF1E88E5),
              fontWeight: FontWeight.w600,
            ),
          ),
        ),
      ],
    );
  }
}

class _CategoryItem {
  const _CategoryItem({
    required this.key,
    required this.title,
    required this.icon,
    required this.color,
  });

  final String key;
  final String title;
  final IconData icon;
  final Color color;
}

class _NewsItem {
  const _NewsItem({
    required this.title,
    required this.subtitle,
    required this.thumbnailUrl,
    required this.coverUrl,
    required this.content,
    required this.rating,
    required this.favoriteCount,
    required this.comments,
  });

  final String title;
  final String subtitle;
  final String thumbnailUrl;
  final String coverUrl;
  final String content;
  final double rating;
  final int favoriteCount;
  final List<String> comments;
}

class _PlaceItem {
  const _PlaceItem({
    required this.title,
    required this.subtitle,
    required this.thumbnailUrl,
    required this.coverUrl,
    required this.description,
    required this.galleryUrls,
    required this.rating,
    required this.favoriteCount,
    required this.comments,
  });

  final String title;
  final String subtitle;
  final String thumbnailUrl;
  final String coverUrl;
  final String description;
  final List<String> galleryUrls;
  final double rating;
  final int favoriteCount;
  final List<String> comments;
}

class _BannerItem {
  const _BannerItem({
    required this.title,
    required this.subtitle,
    required this.image,
  });

  final String title;
  final String subtitle;
  final String image;
}

class _BottomTabItem {
  const _BottomTabItem({required this.icon, required this.label});

  final IconData icon;
  final String label;
}
