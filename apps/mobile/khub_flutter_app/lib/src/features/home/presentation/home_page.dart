import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../account/presentation/change_password_page.dart';
import '../../account/presentation/profile_picture_page.dart';
import '../../auth/presentation/auth_view_model.dart';
import '../../auth/presentation/login_page.dart';
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
    final categories = <_CategoryItem>[
      const _CategoryItem(
        title: 'Tin tuc',
        icon: Icons.newspaper_rounded,
        color: Color(0xFF1E88E5),
      ),
      const _CategoryItem(
        title: 'Dia diem',
        icon: Icons.location_on_rounded,
        color: Color(0xFF00ACC1),
      ),
      const _CategoryItem(
        title: 'Viec lam',
        icon: Icons.work_rounded,
        color: Color(0xFF5E92F3),
      ),
      const _CategoryItem(
        title: 'An uong',
        icon: Icons.restaurant_rounded,
        color: Color(0xFF42A5F5),
      ),
      const _CategoryItem(
        title: 'Gia vang',
        icon: Icons.currency_exchange_rounded,
        color: Color(0xFFFFB300),
      ),
      const _CategoryItem(
        title: 'Gia xang',
        icon: Icons.local_gas_station_rounded,
        color: Color(0xFF7E57C2),
      ),
    ];
    final news = <_NewsItem>[
      const _NewsItem(
        title: 'Nha Trang du lich he 2026',
        subtitle: 'Du bao luong khach tang manh dip le.',
        icon: Icons.beach_access_rounded,
      ),
      const _NewsItem(
        title: 'Gia vang hom nay',
        subtitle: 'Cap nhat bien dong thi truong vang trong ngay.',
        icon: Icons.currency_exchange_rounded,
      ),
      const _NewsItem(
        title: 'Tuyen dung viec lam moi',
        subtitle: 'Nhieu vi tri dang mo tai Khanh Hoa.',
        icon: Icons.badge_rounded,
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
              onTap: () => Navigator.pushNamed(context, ChangePasswordPage.routeName),
            ),
            ListTile(
              leading: const Icon(Icons.person),
              title: Text(vm.t('AbpUi::ProfilePicture')),
              onTap: () => Navigator.pushNamed(context, ProfilePicturePage.routeName),
            ),
            ListTile(
              leading: const Icon(Icons.logout),
              title: Text(vm.t('AbpUi::Logout')),
              onTap: () async {
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
              userName.isEmpty ? '${vm.t('::Welcome')}!' : '${vm.t('::Welcome')}, $userName!',
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
                return Container(
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
                );
              },
            ),
            const SizedBox(height: 22),
            _SectionHeader(title: 'News', actionLabel: 'See more', onTap: () {}),
            const SizedBox(height: 10),
            ...news.map(
              (_NewsItem item) => Container(
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
                    Container(
                      width: 46,
                      height: 46,
                      decoration: BoxDecoration(
                        color: const Color(0xFF42A5F5).withValues(alpha: 0.16),
                        borderRadius: BorderRadius.circular(12),
                      ),
                      child: Icon(item.icon, color: const Color(0xFF1565C0)),
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
    required this.title,
    required this.icon,
    required this.color,
  });

  final String title;
  final IconData icon;
  final Color color;
}

class _NewsItem {
  const _NewsItem({
    required this.title,
    required this.subtitle,
    required this.icon,
  });

  final String title;
  final String subtitle;
  final IconData icon;
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
