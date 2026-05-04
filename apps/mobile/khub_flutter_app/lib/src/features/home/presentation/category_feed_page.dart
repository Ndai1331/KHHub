import 'package:flutter/material.dart';

import '../../news/presentation/news_detail_page.dart';
import '../../places/presentation/place_detail_page.dart';

class CategoryFeedArgs {
  const CategoryFeedArgs({
    required this.categoryTitle,
    required this.items,
  });

  final String categoryTitle;
  final List<CategoryFeedItem> items;
}

class CategoryFeedItem {
  const CategoryFeedItem({
    required this.title,
    required this.subtitle,
    required this.thumbnailUrl,
    required this.coverUrl,
    required this.content,
    required this.rating,
    required this.favoriteCount,
    required this.comments,
    this.galleryUrls = const <String>[],
  });

  final String title;
  final String subtitle;
  final String thumbnailUrl;
  final String coverUrl;
  final String content;
  final double rating;
  final int favoriteCount;
  final List<String> comments;
  final List<String> galleryUrls;
}

class CategoryFeedPage extends StatelessWidget {
  const CategoryFeedPage({super.key});

  static const String routeName = '/category-feed';

  @override
  Widget build(BuildContext context) {
    final args = ModalRoute.of(context)!.settings.arguments! as CategoryFeedArgs;
    final isPlaceCategory = args.categoryTitle == 'Địa điểm';

    return Scaffold(
      appBar: AppBar(title: Text(args.categoryTitle)),
      body: ListView.separated(
        padding: const EdgeInsets.all(16),
        itemCount: args.items.length,
        separatorBuilder: (BuildContext context, int index) => const SizedBox(height: 10),
        itemBuilder: (BuildContext context, int index) {
          final item = args.items[index];
          return GestureDetector(
            onTap: () {
              if (isPlaceCategory) {
                Navigator.pushNamed(
                  context,
                  PlaceDetailPage.routeName,
                  arguments: PlaceDetailArgs(
                    title: item.title,
                    subtitle: item.subtitle,
                    coverUrl: item.coverUrl,
                    thumbnailUrl: item.thumbnailUrl,
                    description: item.content,
                    galleryUrls: item.galleryUrls.isEmpty
                        ? <String>[item.thumbnailUrl, item.coverUrl]
                        : item.galleryUrls,
                    initialRating: item.rating,
                    initialFavoriteCount: item.favoriteCount,
                    initialComments: item.comments,
                  ),
                );
                return;
              }

              Navigator.pushNamed(
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
              );
            },
            child: Container(
              padding: const EdgeInsets.all(12),
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
                    borderRadius: BorderRadius.circular(10),
                    child: Image.network(
                      item.thumbnailUrl,
                      width: 82,
                      height: 82,
                      fit: BoxFit.cover,
                    ),
                  ),
                  const SizedBox(width: 10),
                  Expanded(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: <Widget>[
                        Text(
                          item.title,
                          style: const TextStyle(fontSize: 14, fontWeight: FontWeight.w700),
                          maxLines: 2,
                          overflow: TextOverflow.ellipsis,
                        ),
                        const SizedBox(height: 4),
                        Text(
                          item.subtitle,
                          style: const TextStyle(fontSize: 12, color: Color(0xFF607D8B)),
                          maxLines: 2,
                          overflow: TextOverflow.ellipsis,
                        ),
                        const SizedBox(height: 6),
                        Row(
                          children: <Widget>[
                            const Icon(Icons.favorite_rounded, color: Colors.redAccent, size: 15),
                            const SizedBox(width: 4),
                            Text('${item.favoriteCount}', style: const TextStyle(fontSize: 12)),
                            const SizedBox(width: 10),
                            const Icon(Icons.star_rounded, color: Color(0xFFFFB300), size: 16),
                            const SizedBox(width: 4),
                            Text(item.rating.toStringAsFixed(1), style: const TextStyle(fontSize: 12)),
                          ],
                        ),
                      ],
                    ),
                  ),
                  const Icon(Icons.arrow_forward_ios_rounded, size: 14, color: Color(0xFF1E88E5)),
                ],
              ),
            ),
          );
        },
      ),
    );
  }
}
